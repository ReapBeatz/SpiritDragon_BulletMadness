using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Health playerHealth;
    [SerializeField] public Image totalHP;
    [SerializeField] public Image currentHP;

    private void Start()
    {
        totalHP.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHP.fillAmount = playerHealth.currentHealth / 10.5f;
    }

}
