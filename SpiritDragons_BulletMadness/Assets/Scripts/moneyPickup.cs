using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moeenyPickup : MonoBehaviour, IPickUp
{
    [SerializeField] playerMovement playerScript;
    // Start is called before the first frame update
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickup();
            Destroy(gameObject);
        }
    }

    public void pickup()
    {
        playerScript.money += 50;
    }
}
