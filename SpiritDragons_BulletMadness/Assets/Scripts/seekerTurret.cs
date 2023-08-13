using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekerTurret : MonoBehaviour, IDamage
{
    Rigidbody2D rb;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject health;
    [SerializeField] SpriteRenderer model;
    [SerializeField] GameObject seeker;
    [SerializeField] float launchTimer = 5;
    public float currTimer;

    [SerializeField] int hp;
    Color origColor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        origColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        currTimer += Time.deltaTime;
        shoot();
    }

    void shoot()
    {
        if (currTimer > launchTimer)
        {
            Instantiate(seeker, transform.position, Quaternion.identity);
            currTimer = 0;
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
