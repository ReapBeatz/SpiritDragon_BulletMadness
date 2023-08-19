using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class dashBossAI : MonoBehaviour, IDamage
{
    [SerializeField] GameObject playerPos;
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer model;
    [SerializeField] bulletFire[] bF;
    [SerializeField] int hp;
    float speed = 3;
    [SerializeField] float stoppingDistance;
    [SerializeField] float retreatDistance;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCD = 1f;
    float dashTimer;
    bool isDashing;
    float dashCountCD;
    [SerializeField] float activeMoveSpeed;
    [SerializeField] float dashLength = .5f;

    Color origColor;
    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        origColor = model.material.color;
        gameManager.instance.updateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerPos.transform.position);
        Debug.Log(distance);
        if (dashCountCD <= 0 && dashTimer <= 0)
        {
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

        if (dashCountCD> 0) 
        {
            dashCountCD -= Time.deltaTime;
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
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashDamage());
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
}
