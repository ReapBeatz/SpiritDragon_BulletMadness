using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class buttons : MonoBehaviour
{
    [SerializeField] playerBullet bulletScript;
    [SerializeField] playerMovement playerScript;
    [SerializeField] shooting shootScript;
    [SerializeField] TextMeshProUGUI healthCostText;
    [SerializeField] TextMeshProUGUI damageCostText;
    [SerializeField] TextMeshProUGUI fireRateCostText;
    [SerializeField] TextMeshProUGUI dashCostText;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject pauseButtons;
    AudioSource aSource;
    public int healthCost;
    public int damageCost;
    public int fireRateCost;
    public int dashCost;
    int maxHealthUpgrade = 0;
    int maxDamageUpgrade = 0;
    int maxFireRateUpgrade = 0;
    int maxDashUpgrade = 0;


    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        healthCostText.text = healthCost.ToString("F0");
        damageCostText.text = damageCost.ToString("F0");
        fireRateCostText.text = fireRateCost.ToString("F0");
        dashCostText.text = dashCost.ToString("F0");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        shootScript = GameObject.FindGameObjectWithTag("Player").GetComponent<shooting>();
    }

    private void Update()
    {
        if (maxHealthUpgrade == 5)
        {
            healthCostText.text = "Max";
        }
        if (maxDamageUpgrade == 5)
        {
            damageCostText.text = "Max";
        }
        if (maxFireRateUpgrade == 5)
        {
            fireRateCostText.text = "Max";
        }
        if (maxDashUpgrade == 5)
        {
            dashCostText.text = "Max";
        }
    }

    public void resume()
    {
        aSource.Play();
        gameManager.instance.stateUnpaused();
    }

    public void options()
    {
        aSource.Play();
        optionsPanel.SetActive(true);
        pauseButtons.SetActive(false);
    }

    public void respawn()
    {
        aSource.Play();
        gameManager.instance.stateUnpaused();
        gameManager.instance.playerScript.spawnPlayer();
    }

    public void restart()
    {
        aSource.Play();
        gameManager.instance.stateUnpaused();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMainMenu()
    {
        aSource.Play();
        SceneManager.LoadSceneAsync(0);
    }

    public void upgradeHealth() 
    {
        if (maxHealthUpgrade < 5)
        {
            if (playerScript.money >= healthCost)
            {
                playerScript.money -= healthCost;
                playerScript.hp += 5;
                playerScript.hpOrig += 5;
                healthCost += 250;
                healthCostText.text = healthCost.ToString("F0");
                playerScript.updatePlayerUI();
                maxHealthUpgrade++;
            }
        }
    }

    public void upgradeDamage()
    {
        if (maxDamageUpgrade < 5)
        {
            if (playerScript.money >= damageCost)
            {
                playerScript.money -= damageCost;
                bulletScript.damage++;
                damageCost += 100;
                damageCostText.text = damageCost.ToString("F0");
                playerScript.updatePlayerUI();
                maxDamageUpgrade++;
            }
        }
    }

    public void upgradeFireRate()
    {
        if (maxFireRateUpgrade < 5)
        {
            if (playerScript.money >= fireRateCost)
            {
                playerScript.money -= fireRateCost;
                shootScript.fireRate++;
                fireRateCost += 75;
                fireRateCostText.text = fireRateCost.ToString("F0");
                playerScript.updatePlayerUI();
                maxFireRateUpgrade++;
            }
        }
    }

    public void upgradeDash()
    {
        if (maxDashUpgrade < 5)
        {
            if (playerScript.money >= dashCost)
            {
                playerScript.money -= dashCost;
                playerScript.dashLength += .025f;
                dashCost += 200;
                dashCostText.text = dashCost.ToString("F0");
                playerScript.updatePlayerUI();
                maxDashUpgrade++;
            }
        }
    }
}
