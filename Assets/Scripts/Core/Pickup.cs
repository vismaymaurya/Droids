using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public WeaponConfig weaponConfig;
    [HideInInspector]
    public Sprite weaponIcon;

    private void Awake()
    {
        weaponIcon = weaponConfig.Icon;
    }
}
