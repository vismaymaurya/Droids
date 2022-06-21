using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public GameObject player;
    public float speed;

    float xOffset;
    PlayerCtrl playerCtrl;

    Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    private void Update()
    {
        //xOffset = .005f;
        if (!playerCtrl.isStuck)
        {
            xOffset += Input.GetAxisRaw("Horizontal") * speed;

            mat.SetTextureOffset("_MainTex", new Vector2(xOffset, 0));
        }

    }
}
