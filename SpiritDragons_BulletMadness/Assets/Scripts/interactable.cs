using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            gameManager.instance.activeMenu = gameManager.instance.upgradeMenu;
            gameManager.instance.activeMenu.SetActive(true);
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
