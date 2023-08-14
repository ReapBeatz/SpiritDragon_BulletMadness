using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] playerMovement playerScript;

    [SerializeField] float bulletForce = 20f;
    public float fireRate;
    float currTime;
    // Update is called once per frame
    void Update()
    {
        if (playerScript.isDead != true)
        {
            currTime += Time.deltaTime;
            float nextTimeToFire = 1 / fireRate;
            if (currTime >= nextTimeToFire && Input.GetButton("Fire1"))
            {
                Shoot();
                currTime = 0;
            }
        } 
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
