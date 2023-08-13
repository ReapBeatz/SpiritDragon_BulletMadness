using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elecTowerAI : MonoBehaviour , IDamage
{
    Rigidbody2D rb;
    Color origColor;
    //[SerializeField] GameObject playerPos;
    [SerializeField] SpriteRenderer model;
    [SerializeField] GameObject[] fireModes;
    [SerializeField] mechBossAI mB;

    [SerializeField] int hp;
    int halfHp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //playerPos = GameObject.FindGameObjectWithTag("Player");
        origColor = model.material.color;
        halfHp = hp / 2;
        mB = GameObject.FindGameObjectWithTag("mechBoss").GetComponent<mechBossAI>();
        mB.elecTowers++;
    }

    // Update is called once per frame
    void Update()
    {
        mode();
    }

    void mode()
    {
        if (hp <= halfHp)
        {
            fireModes[0].SetActive(false); 
            fireModes[1].SetActive(true);
        }
    }

    public void takeDamage(int dmgAmount)
    {
        hp -= dmgAmount;
        if (hp <= 0)
        {
            mB.elecTowers--;
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashDamage());
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
}
