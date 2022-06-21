using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckCtrl : MonoBehaviour
{
    public GameObject player;

    PlayerCtrl playerCtrl;

    private void Start()
    {
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    private void Update()
    {
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCtrl.isStuck = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerCtrl.isStuck = false;
    }
}
