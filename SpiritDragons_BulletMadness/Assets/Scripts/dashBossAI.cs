using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dashBossAI : MonoBehaviour, IDamage
{
    [SerializeField] GameObject playerPos;
    [SerializeField] playerMovement playerScript;
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer model;
    [SerializeField] bulletFire[] bF;
    [SerializeField] AudioClip dashClip;
    AudioSource aSource;
    [SerializeField] int hp;
    public int hpOrig;
    [SerializeField] Image HPBar;
    float speed = 3;
    [SerializeField] float stoppingDistance;
    [SerializeField] float retreatDistance;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCD = 1f;
    float dashTimer;
    bool isDashing;
    bool notDead = true;
    float dashCountCD;
    [SerializeField] float activeMoveSpeed;
    [SerializeField] float dashLength = .5f;
    PolygonCollider2D pc;

    Color origColor;
    // Start is called before the first frame update
    void Start()
    {
        hpOrig = hp;
        activeMoveSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();
        aSource = GetComponent<AudioSource>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        origColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (notDead)
        {
            float distance = Vector2.Distance(transform.position, playerPos.transform.position);
            Debug.Log(distance);
            if (dashCountCD <= 0 && dashTimer <= 0)
            {
                aSource.PlayOneShot(dashClip);
                activeMoveSpeed = dashSpeed;
                dashTimer = dashLength;
                model.material.color = Color.cyan;
                isDashing = true;
            }
            if (distance > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.transform.position, activeMoveSpeed * Time.deltaTime);
                shoot();
            }
            if (distance < stoppingDistance && distance > retreatDistance && !isDashing)
            {
                transform.position = this.transform.position;
                shoot();
            }
            if (distance < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.transform.position, -activeMoveSpeed * Time.deltaTime);
                shoot();
            }

            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    isDashing = false;
                    activeMoveSpeed = speed;
                    dashCountCD = dashCD;
                    model.material.color = origColor;
                }
            }

            if (dashCountCD > 0)
            {
                dashCountCD -= Time.deltaTime;
            }
        }
        
    }

    void FixedUpdate()
    {
        Vector2 lookDir = playerPos.transform.position - rb.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void shoot()
    {
        foreach (var b in bF)
        {
            b.currTimer += Time.deltaTime;
            if (b.currTimer > b.fireRate)
            {
                b.currTimer = 0;
                b.Shoot();
            }
        }
    }
    public void takeDamage(int dmgAmount)
    {
        hp -= dmgAmount;
        if (hp <= 0)
        {
            playerScript.hasDash = true;
            playerScript.money += 500;
            playerScript.updatePlayerUI();
            StartCoroutine(LoadNextScene());
        }
        else
        {
            StartCoroutine(flashDamage());
            updateHPBar();
        }
    }

    IEnumerator LoadNextScene()
    {
        pc.enabled = false;
        model.enabled = false;
        notDead = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    void updateHPBar()
    {
        HPBar.fillAmount = (float)hp/hpOrig;
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
}
