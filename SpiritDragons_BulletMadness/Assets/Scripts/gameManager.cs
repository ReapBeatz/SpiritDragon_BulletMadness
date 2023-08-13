using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject player;
    public playerMovement playerScript;
    public GameObject playerSpawnPos;

    public GameObject playerFlashDamagePanel;
    public bool isPaused;
    float timescaleOrig;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerMovement>();
        timescaleOrig = Time.timeScale;
        playerSpawnPos = GameObject.FindGameObjectWithTag("playerSpawnPos");
    }

    public IEnumerator playerFlashDamage()
    {
        playerFlashDamagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerFlashDamagePanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Cancel") && activeMenu == null)
        //{
        //    statePause();
        //    activeMenu = pauseMenu;
        //    activeMenu.SetActive(isPaused);
        //}
    }

    //public void statePause()
    //{
    //    Time.timeScale = 0;
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.Confined;
    //    isPaused = !isPaused;
    //}

    public void stateUnpaused()
    {
        Time.timeScale = timescaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = !isPaused;
        //activeMenu.SetActive(false);
        //activeMenu = null;
    }

    public void youLose()
    {
        playerScript.isDead = true;
        playerScript.model.enabled = false;
        //playerScript.enabled = false;
        playerScript.pc.enabled = false;
        //activeMenu = loseMenu;
        //activeMenu.SetActive(true);
    }
}
