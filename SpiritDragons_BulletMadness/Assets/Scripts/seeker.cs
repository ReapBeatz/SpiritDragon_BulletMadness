using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seeker : MonoBehaviour
{
    Transform target;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotateSpeed = 200f;
    [SerializeField] int damage = 5;
    Rigidbody2D rb;
    [SerializeField] float launchTimer = 1;
    [SerializeField] float destroyTimer = 4;
    public float currTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currTimer += Time.deltaTime;
        if (currTimer > launchTimer)
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
        if (currTimer > destroyTimer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage damageable = collision.GetComponent<IDamage>();
        if (damageable != null)
        {
            damageable.takeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
