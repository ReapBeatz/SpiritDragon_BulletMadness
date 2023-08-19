using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pivotRotate : MonoBehaviour
{
    Vector3 origRotation = new Vector3(0, 0, -225f);
    Vector3 endRotation = new Vector3(0, 0, -135);
    [SerializeField] float speed = 1f;

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion orig = Quaternion.Euler(origRotation);
        Quaternion endRot = Quaternion.Euler(endRotation);
        float lerp = .5f * (1f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup *speed));
        transform.localRotation = Quaternion.Lerp(orig, endRot, lerp);
    }
}
