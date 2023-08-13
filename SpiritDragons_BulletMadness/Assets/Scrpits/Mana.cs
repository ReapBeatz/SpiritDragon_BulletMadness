using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    [SerializeField] private float startingMana;

    public float CurrentMana { get; private set; }

    private void Awake()
    {
        CurrentMana = startingMana;
    }

    public float GetStartingMana()
    {
        return startingMana;
    }
}
