using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AquaBossAI : MonoBehaviour, IDamage
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    Rigidbody2D rb;
    Transform currentPoint;
    [SerializeField] SpriteRenderer model;
    [SerializeField] bulletFire[] bF;
    [SerializeField] int hp;
    [SerializeField] float speed;
    [SerializeField] modSeekerTurret mST;
    [SerializeField] Image HPBar;
    Color origColor;
    public int hpOrig;
    public float speedOrig;
    public float launchTimerOrig;
    [SerializeField] float rageTimer;
    public float currTimer;
    public bool inRage = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        origColor = model.material.color;
        hpOrig = hp;
        speedOrig = speed;
        launchTimerOrig = mST.launchTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inRage)
        {
            currTimer += Time.deltaTime;
        }

        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            //flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            //flip();
            currentPoint = pointB.transform;
        }
        //if (inRage) 
        //{
        //    StartCoroutine(rageCD());
        //    inRage = false;
        //    currTimer = 0;
        //}
        if (hp <= hpOrig/4 || currTimer > rageTimer) 
        {
            inRage = true;
            if(inRage) 
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
        speed = speedOrig;
        mST.launchTimer = launchTimerOrig;
        model.material.color = origColor;
        foreach (var b in bF)
        {
            b.fireRate = .5f;
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
        speed = 6;
        mST.launchTimer = 2;
        model.material.color = new Color(1f, 0f, 0f, .75f);
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

    void flip() 
    {    
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
}
