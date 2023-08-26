using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour , IDamage
{
    public int hp;
    public int shield;
    public int money;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;
    [SerializeField] AudioClip dashClip;
    [SerializeField] AudioClip rageClip;
    [SerializeField] AudioClip hurtClip1;
    [SerializeField] AudioClip hurtClip2;
    [SerializeField] AudioClip shieldClip;
    [SerializeField] playerBullet bulletDamage;
    shooting shootScript;
    public SpriteRenderer model;
    public PolygonCollider2D pc;

    float activeMoveSpeed;
    [SerializeField] float dashSpeed;
    public float dashLength = .5f;
    [SerializeField] float dashCD = 1f;
    public float dashTimer;
    [SerializeField] float rageCD;
    float rageTimer;
    [SerializeField] float shieldCD;
    float shieldTimer;
    public int hpOrig;
    public int maxShield;
    float dashCountCD;
    public bool isDead = false;
    Vector2 movement;
    Vector2 mousePos;
    Color origColor;
    float moveSpeedOrig;
    float fireRateOrig;
    public bool canUse;
    AudioSource audioSource;

    public bool hasDash = false;
    public bool hasRage = false;
    public bool hasShield = false;
    public int hurtNum;

    //public string damageKey = "Damage";
    //public string fireRateKey = "fireRate";
    //public string maxHealthKey = "maxHealth";
    //public string moneyKey = "money";
    //public string dashLengthKey = "dashLength";
    void Awake() 
    {
        shootScript = GetComponent<shooting>();
        //bulletDamage.damage = PlayerPrefs.GetInt(damageKey, 2);
        //shootScript. fireRate = PlayerPrefs.GetFloat(fireRateKey, 3f);
        //hpOrig = PlayerPrefs.GetInt(maxHealthKey, 25);
        //money =  PlayerPrefs.GetInt(moneyKey, 0);
        //dashLength = PlayerPrefs.GetFloat(dashLengthKey, .1f);
    }
    void Start()
    {
        gameManager.instance.GameReset();
        audioSource = GetComponent<AudioSource>();
        pc = GetComponent<PolygonCollider2D>();
        moveSpeedOrig = moveSpeed;
        fireRateOrig = shootScript.fireRate;
        activeMoveSpeed = moveSpeed;
        origColor = model.material.color;
        hpOrig = hp;
        updatePlayerUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead != true)
        {
            rageTimer += Time.deltaTime;
            shieldTimer += Time.deltaTime;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Fire2"))
            {
                if (dashCountCD <= 0 && dashTimer <= 0 && hasDash) 
                {
                    audioSource.PlayOneShot(dashClip);
                    activeMoveSpeed = dashSpeed;
                    dashTimer = dashLength;
                    gameObject.layer = LayerMask.NameToLayer("Invulnerable");
                    model.material.color = Color.cyan;
                }
            }

            if (shieldTimer > shieldCD && hasShield)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    audioSource.PlayOneShot(shieldClip);
                    shield += 10;
                    shieldTimer = 0;
                    updatePlayerUI();
                }
            }

            if (rageTimer > rageCD && hasRage)
            {
                canUse = true;
                if (Input.GetKeyDown(KeyCode.E))
                { 
                    if (canUse)
                    {
                        canUse = false;
                        StartCoroutine(Rage());
                    }
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
        hurtNum = Random.Range(0, 2);
        if (hurtNum == 0)
        {
            audioSource.PlayOneShot(hurtClip1);
        }
        else if (hurtNum == 1) 
        {
            audioSource.PlayOneShot(hurtClip2);
        }        
        if (shield <= 0)
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
        else
        {
            shield-= dmgAmount;
            StartCoroutine(gameManager.instance.playerFlashDamage());
            updatePlayerUI();
        }
    }

    public void updatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)hp / hpOrig;
        gameManager.instance.playerShieldBar.fillAmount = (float)shield / maxShield;
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
        updatePlayerUI();
    }

    IEnumerator Rage() 
    {
        audioSource.PlayOneShot(rageClip);
        rageTimer = 0;
        model.color = new Color(1, 0, 1, 1);
        moveSpeed = 8;
        shootScript.fireRate *= 2;
        yield return new WaitForSeconds(3);
        model.color = origColor;
        moveSpeed = moveSpeedOrig;
        shootScript.fireRate = fireRateOrig;
        canUse = true;
    }
}
