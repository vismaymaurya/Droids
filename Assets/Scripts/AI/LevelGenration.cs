using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenration : MonoBehaviour
{
    public GameObject[] objects;
    [Range(1,300)]
    public float trigger = 1;

    private void Awake()
    {
       //Genrator();
    }

    private void OnValidate()
    {
        if (trigger>2)
        {
            //Genrator();
        }
    }

    public void Genrator()
    {
        int rand = Random.Range(0, objects.Length);

        var ints = Instantiate(objects[rand], transform.position, Quaternion.identity);
        ints.transform.parent = this.transform;
    }
}
