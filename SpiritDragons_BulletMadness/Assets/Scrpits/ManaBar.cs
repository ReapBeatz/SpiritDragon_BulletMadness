using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Mana playerMana;
    [SerializeField] public Image totalMana;
    [SerializeField] public Image currentMana;

    private void Start()
    {
        UpdateManaBar();
    }

    private void Update()
    {
        UpdateManaBar();
    }

    private void UpdateManaBar()
    {
        if (playerMana != null)
        {
            float startingMana = playerMana.GetStartingMana(); 
            totalMana.fillAmount = playerMana.CurrentMana / startingMana;
            currentMana.fillAmount = playerMana.CurrentMana / startingMana;
        }
    }

}