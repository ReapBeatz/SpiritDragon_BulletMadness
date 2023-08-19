using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class mainBossAI : MonoBehaviour, IDamage
{
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer model;
    [SerializeField] bulletFire[] bF;
    [SerializeField] int hp;
    [SerializeField] Image HPBar;
    Color origColor;
    public int hpOrig;
    [SerializeField] float rageTimer;
    public float currTimer;
    public bool inRage = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        origColor = model.material.color;
        hpOrig = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= hpOrig / 8 || currTimer > rageTimer)
        {
            inRage = true;
            if (inRage)
            {
                rage();
                StartCoroutine(rageCD());
            }
        }
        else if (!inRage)
        {
            shoot();
        }
    }

    void shoot()
    {
        model.material.color = origColor;
        foreach (var b in bF)
        {
            b.fireRate = .1f;
            b.currTimer += Time.deltaTime;
            if (b.currTimer > b.fireRate)
            {
                b.currTimer = 0;
                b.Shoot();
            }
        }
    }

    void rage()
    {
        model.material.color = new Color(1f, 0f, 0f, .75f);
        foreach (var b in bF)
        {
            b.fireRate = .05f;
            b.currTimer += Time.deltaTime;
            if (b.currTimer > b.fireRate)
            {
                b.currTimer = 0;
                b.Shoot();
            }
        }
    }

    IEnumerator rageCD()
    {
        yield return new WaitForSeconds(3);
        inRage = false;
        currTimer = 0;
    }


    public void takeDamage(int dmgAmount)
    {
        hp -= dmgAmount;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashDamage());
            updateHPBar();
        }
    }

    void updateHPBar()
    {
        HPBar.fillAmount = (float)hp / hpOrig;
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
}
