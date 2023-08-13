using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class lazer : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage damageable = collision.GetComponent<IDamage>();
        if (damageable != null)
        {
            damageable.takeDamage(damage);
        }
    }
}
