using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFire : MonoBehaviour
{
    [SerializeField] playerMovement playerScript;
    [SerializeField] GameObject bulletPrefab;
    public float fireRate;
    public float fireDelay;
    public float bulletForce = 20f;
    public float currTimer;

    public void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
    }
    public void Shoot()
    {
        if (playerScript.isDead != true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
        }     
    }
}
