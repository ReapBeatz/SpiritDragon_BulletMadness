using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public int damage;
    [SerializeField] float destroyTime;
    //public GameObject hitEffect;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        IDamage damageable = collision.gameObject.GetComponent<IDamage>();
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
