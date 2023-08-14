using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour , IDamage
{
    public int hp;
    public int money;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;
    public SpriteRenderer model;
    public PolygonCollider2D pc;

    float activeMoveSpeed;
    [SerializeField] float dashSpeed;
    public float dashLength = .5f;
    [SerializeField] float dashCD = 1f;
    public float dashTimer;
    public int hpOrig;
    float dashCountCD;
    public bool isDead = false;
    Vector2 movement;
    Vector2 mousePos;
    Color origColor;
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        pc = GetComponent<PolygonCollider2D>();
        origColor = model.material.color;
        hpOrig = hp;
        updatePlayerUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead != true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Fire2"))
            {
                if (dashCountCD <= 0 && dashTimer <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashTimer = dashLength;
                    gameObject.layer = LayerMask.NameToLayer("Invulnerable");
                    model.material.color = Color.cyan;
                }
            }

            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCountCD = dashCD;
                    gameObject.layer = LayerMask.NameToLayer("Player");
                    model.material.color = origColor;
                }
            }

            if (dashCountCD > 0)
            {
                dashCountCD -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                spawnPlayer();
                gameManager.instance.activeMenu.SetActive(false);
                gameManager.instance.activeMenu = null;
            }
            movement.x = 0;
            movement.y = 0;
        }
    }

    public void takeDamage(int dmgAmount)
    {
        hp -= dmgAmount;
        StartCoroutine(dmgInvul());
        StartCoroutine(gameManager.instance.playerFlashDamage());
        updatePlayerUI();
        if (hp <= 0)
        {
            gameManager.instance.youLose();
        }
    }

    public void updatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)hp / hpOrig;
        gameManager.instance.moneyText.text = money.ToString("F0");
    }

    IEnumerator dmgInvul()
    {
        model.material.color = Color.green;
        gameObject.layer = LayerMask.NameToLayer("Invulnerable");
        yield return new WaitForSeconds(1);
        gameObject.layer = LayerMask.NameToLayer("Player");
        model.material.color = origColor;
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position +  movement.normalized * activeMoveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void spawnPlayer()
    {
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        hp = hpOrig;
        model.enabled = true;
        pc.enabled = true;
        isDead = false;
    }
}
