using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerFireMoode : MonoBehaviour
{
    [SerializeField] GameObject playerPos;
    Rigidbody2D rb;

    [SerializeField] bulletFire[] bF;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void FixedUpdate()
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
            if (b.currTimer > b.fireDelay)
            {
                if (b.currTimer > b.fireRate)
                {
                    b.currTimer = 0;
                    b.Shoot();
                }
            }
        }
    }
}
