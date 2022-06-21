using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChecker : MonoBehaviour
{
    [HideInInspector]
    public string weaponName = "";
    [HideInInspector]
    public WeaponConfig weapon;
    [HideInInspector]
    public Sprite weaponIcon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            weaponName = collision.gameObject.name;
            weapon = collision.gameObject.GetComponent<Pickup>().weaponConfig;
            weaponIcon = collision.gameObject.GetComponent<Pickup>().weaponIcon;
            Destroy(collision.gameObject);
        }
        
    }
}
