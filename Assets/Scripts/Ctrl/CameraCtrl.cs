using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform player;
    public float yOffset;

    private void Update()
    {
        //for only x axis
        //transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

        //for x&y axis
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
        }
    }
}
