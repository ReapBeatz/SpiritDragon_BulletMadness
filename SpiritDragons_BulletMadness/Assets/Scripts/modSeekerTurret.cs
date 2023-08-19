using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modSeekerTurret : MonoBehaviour
{
    [SerializeField] GameObject seeker;
    [SerializeField] AquaBossAI ab;
    public float launchTimer;
    public float currTimer;

    // Update is called once per frame
    void Update()
    {
        currTimer += Time.deltaTime;
        shoot();
    }

    void shoot()
    {
        if (currTimer > launchTimer) 
        {
            Instantiate(seeker, transform.position, Quaternion.identity);
            if (ab.inRage)
            {
                Instantiate(seeker, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
            }
            currTimer = 0;
        }
    }
}
