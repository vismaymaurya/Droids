using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenration : MonoBehaviour
{
    public GameObject[] objects;

    private void Start()
    {
        Genrator();
    }

    private void Genrator()
    {
        int rand = Random.Range(0, objects.Length);

        var ints = Instantiate(objects[rand], transform.position, Quaternion.identity);
        ints.transform.parent = this.transform;
    }
}
