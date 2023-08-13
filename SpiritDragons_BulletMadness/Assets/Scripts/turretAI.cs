using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAI : MonoBehaviour , IDamage
{
    Rigidbody2D rb;
    [SerializeField] GameObject playerPos;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject health;
    [SerializeField] SpriteRenderer model;
    [SerializeField] bulletFire[] bF;

    [SerializeField] int hp;
    Color origColor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        origColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    private void FixedUpdate()
    {
        if (tag == "RotateTurret")
        {
            Vector2 lookDir = playerPos.transform.position - rb.transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        } 
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
            int drop = Random.Range(0, 2);
            switch (drop)
            {
                case 0:
                    dropCoin();
                    break;
                case 1:
                    dropHealth();
                    break;
            }
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashDamage());
        }
    }
    void dropCoin()
    {
        Vector2 position = transform.position;
        Instantiate(coin, position, Quaternion.identity);
    }

    void dropHealth()
    {
        Vector2 position = transform.position;
        Instantiate(health, position, Quaternion.identity);
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
}
