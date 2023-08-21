using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class bomb : MonoBehaviour
{
    [SerializeField] float fuseTime;
    [SerializeField] float liveTime;
    [SerializeField] GameObject wave;
    [SerializeField] SpriteRenderer sR;
    AudioSource aSource;
    public float currTimer;
    public bool hasLanded = false;
    //public GameObject hitEffect;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        currTimer += Time.deltaTime;
        if (currTimer > fuseTime && !hasLanded)
        {
            wave.SetActive(true);
            currTimer = 0;
            hasLanded = true;
            sR.enabled = false;
            aSource.Play();
        }
        if (currTimer > liveTime && hasLanded)
        {
            Destroy(gameObject);
        }
    }
}
