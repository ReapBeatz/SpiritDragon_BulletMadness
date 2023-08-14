using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] playerMovement playerHealth;
    [SerializeField] Image totalHP;
    [SerializeField] Image currentHP;

    private void Start()
    {
        totalHP.fillAmount = playerHealth.hp;
    }

    private void Update()
    {
        currentHP.fillAmount = playerHealth.hp / playerHealth.hpOrig;
    }

}
