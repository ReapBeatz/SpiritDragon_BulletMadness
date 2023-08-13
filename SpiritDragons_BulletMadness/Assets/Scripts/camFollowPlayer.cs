using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z); 
    }
}
