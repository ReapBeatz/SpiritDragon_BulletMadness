using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mechBossAI : MonoBehaviour, IDamage
{
    Rigidbody2D rb;
    Color origColor;
    //[SerializeField] GameObject playerPos;
    [SerializeField] SpriteRenderer model;
    [SerializeField] GameObject[] fireModes;
    [SerializeField] GameObject[] lazerPrefab;

    [SerializeField] int hp;
    public int elecTowers;

    int halfHp;
    public float currTimer;
    public float lTimer;
    [SerializeField]float bombTimer;
    [SerializeField]float lazerTimer;
    bool canDropBomb = true;
    bool canFireLazer = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //playerPos = GameObject.FindGameObjectWithTag("Player");
        origColor = model.material.color;
        halfHp = hp / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (elecTowers == 0)
        {
            fireModes[0].SetActive(true);
            fireModes[1].SetActive(true);
            elecTowers--;

        }
        if (elecTowers == -1)
        {
            currTimer += Time.deltaTime;
            lTimer += Time.deltaTime;
            if (currTimer > bombTimer && canDropBomb == true)
            {
                StartCoroutine(dropBombs());
            }
            if (lTimer > lazerTimer && fireModes[2].activeInHierarchy == true)
            {
                StartCoroutine(lazer());
                
            }
            if (lTimer > lazerTimer && canFireLazer == true)
            {
                StartCoroutine(lazer());
            }
        }
    }

    void mode()
    {
        //if (hp <= halfHp)
        //{
        //    fireModes[0].SetActive(false);
        //    fireModes[1].SetActive(true);
        //}
    }

    public void takeDamage(int dmgAmount)
    {
        if (elecTowers == -1)
        {
            hp -= dmgAmount;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(flashDamage());
            }
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }

    IEnumerator dropBombs()
    {
        canDropBomb = false;
        fireModes[0].SetActive(false);
        fireModes[1].SetActive(false);
        fireModes[2].SetActive(true);
        yield return new WaitForSeconds(10f);
        fireModes[2].SetActive(false);
        fireModes[0].SetActive(true);
        fireModes[1].SetActive(true);
        canDropBomb = true;
        currTimer = 0;
        canFireLazer = false;
    }
    IEnumerator lazer() 
    {
        canFireLazer = true;
        lazerPrefab[0].SetActive(true);
        lazerPrefab[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        lazerPrefab[0].SetActive(false);
        lazerPrefab[1].SetActive(false);
        lTimer = 0;
    }
}
