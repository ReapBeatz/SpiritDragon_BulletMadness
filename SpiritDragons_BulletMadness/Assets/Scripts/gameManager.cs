using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject player;
    public playerMovement playerScript;
    public GameObject playerSpawnPos;

    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject upgradeMenu;
    public GameObject playerFlashDamagePanel;
    public TextMeshProUGUI enemiesRemainingText;
    public TextMeshProUGUI moneyText;
    public Image playerHPBar;
    public int enemiesRemaining;
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
        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            statePause();
            activeMenu = pauseMenu;
            activeMenu.SetActive(isPaused);
        }
    }

    public void statePause()
    {
        Time.timeScale = 0;
        isPaused = !isPaused;
    }

    public void stateUnpaused()
    {
        Time.timeScale = timescaleOrig;
        isPaused = !isPaused;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void updateGameGoal(int amount)
    {
        enemiesRemaining += amount;
        enemiesRemainingText.text = enemiesRemaining.ToString("F0");
        if (enemiesRemaining <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            StartCoroutine(winMenuTimer());
        }
    }

    IEnumerator winMenuTimer()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        statePause();
    }

    public void youLose()
    {
        playerScript.isDead = true;
        playerScript.model.enabled = false;
        //playerScript.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Invulnerable");
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        //activeMenu = loseMenu;
        //activeMenu.SetActive(true);
    }
}
