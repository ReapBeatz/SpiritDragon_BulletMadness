using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour, IPickUp
{
    [SerializeField] playerMovement playerScript;
    [SerializeField] GameObject playerPos;
    // Start is called before the first frame update
    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, playerPos.transform.position);
        Debug.Log(distance);
        if (distance > 20)
        {
            Destroy(gameObject);
        }
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
        playerScript.hp += playerScript.hpOrig / 5;
        if (playerScript.hp > playerScript.hpOrig) 
        {
            playerScript.hp = playerScript.hpOrig;
        }
        playerScript.updatePlayerUI();
    }
}
