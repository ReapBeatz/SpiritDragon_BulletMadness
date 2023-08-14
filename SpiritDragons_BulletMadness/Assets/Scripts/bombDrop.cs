using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class bombDrop : MonoBehaviour
{
    [SerializeField] GameObject playerPos;
    Rigidbody2D rb;
    //[SerializeField] SpriteRenderer model;
    [SerializeField] bulletFire[] bF;

    //[SerializeField] int hp;
    [SerializeField] float speed;
    //[SerializeField] float stoppingDistance;
    //[SerializeField] float retreatDistance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerPos.transform.position);
        Debug.Log(distance);
        transform.position = Vector2.MoveTowards(transform.position, playerPos.transform.position, speed * Time.deltaTime);
        if (distance < .5)
        {
            shoot();
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
}
