using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class advanceCheck : MonoBehaviour
{
    [SerializeField] string say;
    [SerializeField] int nextSceneNum;
    playerMovement playerScript;
    shooting shootScript;
    [SerializeField] playerBullet bulletScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        shootScript = GameObject.FindGameObjectWithTag("Player").GetComponent<shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameManager.instance.enemiesRemaining > 0)
        {
            gameManager.instance.dialougeText.text = say;
        }
        if (collision.CompareTag("Player") && gameManager.instance.enemiesRemaining <= 0 && tag == "nextLevelCheck")
        {
            PlayerPrefs.SetInt(playerScript.moneyKey, playerScript.money);
            PlayerPrefs.SetInt(playerScript.maxHealthKey, playerScript.hpOrig);
            PlayerPrefs.SetInt(playerScript.damageKey, bulletScript.damage);
            PlayerPrefs.SetFloat(playerScript.fireRateKey, shootScript.fireRate);
            PlayerPrefs.SetFloat(playerScript.dashLengthKey, playerScript.dashLength);

            SceneManager.LoadSceneAsync(nextSceneNum);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.instance.dialougeText.text = " ";
        }
    }
}
