using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        healthCostText.text = healthCost.ToString("F0");
        damageCostText.text = damageCost.ToString("F0");
        fireRateCostText.text = fireRateCost.ToString("F0");
        dashCostText.text = dashCost.ToString("F0");
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

    public void quit()
    {
        aSource.Play();
        Application.Quit();
    }

    public void upgradeHealth() 
    {
        if (playerScript.money >= healthCost)
        {
            playerScript.money -= healthCost;
            playerScript.hp += 5;
            playerScript.hpOrig += 5;
            healthCost += 250;
            healthCostText.text = healthCost.ToString("F0");
            playerScript.updatePlayerUI();
        }
    }

    public void upgradeDamage()
    {
        if (playerScript.money >= damageCost)
        {
            playerScript.money -= damageCost;
            bulletScript.damage += 2;
            damageCost += 100;
            damageCostText.text = damageCost.ToString("F0");
            playerScript.updatePlayerUI();
        }
    }

    public void upgradeFireRate()
    {
        if (playerScript.money >= fireRateCost)
        {
            playerScript.money -= fireRateCost;
            shootScript.fireRate++;
            fireRateCost += 75;
            fireRateCostText.text = fireRateCost.ToString("F0");
            playerScript.updatePlayerUI();
        }
    }

    public void upgradeDash()
    {
        if (playerScript.money >= dashCost)
        {
            playerScript.money -= dashCost;
            playerScript.dashLength += .25f;
            dashCost += 200;
            dashCostText.text = dashCost.ToString("F0");
            playerScript.updatePlayerUI();
        }
    }
}
