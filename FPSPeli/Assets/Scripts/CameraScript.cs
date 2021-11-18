using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float xSet, ySet, Zset;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(xSet, ySet, Zset);
        transform.LookAt(player.transform.position);
    }
}

