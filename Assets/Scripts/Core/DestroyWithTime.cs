using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTime : MonoBehaviour
{
    private void Update()
    {
        Destroy(this.gameObject, 3);
    }
}
