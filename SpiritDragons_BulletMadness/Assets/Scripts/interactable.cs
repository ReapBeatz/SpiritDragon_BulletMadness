using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    [SerializeField] playerMovement playerScript;
    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange && gameObject.tag == "UpgradeStation")
        {
            gameManager.instance.activeMenu = gameManager.instance.upgradeMenu;
            gameManager.instance.activeMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F) && inRange && gameObject.tag == "Chest")
        {
            playerScript.money += 100;
            playerScript.updatePlayerUI();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            if (gameManager.instance.activeMenu != null)
            {
                gameManager.instance.activeMenu.SetActive(false);
                gameManager.instance.activeMenu = null;
            }
        }
    }
}
