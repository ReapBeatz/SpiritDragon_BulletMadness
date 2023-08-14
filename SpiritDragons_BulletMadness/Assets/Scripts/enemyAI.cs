using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class enemyAI : MonoBehaviour, IDamage
{
    [SerializeField] GameObject playerPos;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject health;
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer model;
    [SerializeField] bulletFire[] bF;
    
    [SerializeField] int hp;
    [SerializeField] float speed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float retreatDistance;
    Color origColor;
    // Start is called before the first frame update
    void Start()
    {
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
        if (distance > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.transform.position, speed * Time.deltaTime);
            shoot();
        }
        else if(distance < stoppingDistance && distance > retreatDistance) 
        {
            transform.position = this.transform.position;
            shoot();
        }
        else if(distance < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.transform.position, -speed * Time.deltaTime);
            shoot();
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
            int drop = Random.Range(0, 2);
            Debug.Log(drop);
            switch (drop)
            {
                case 0:
                    dropCoin();
                    break;
                case 1:
                    dropHealth();
                    break;
            }
            gameManager.instance.updateGameGoal(-1);
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
