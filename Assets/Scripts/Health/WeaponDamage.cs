using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [HideInInspector]
    public float projectileDamage;

    private Transform player;

    public static WeaponDamage instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        projectileDamage = player.GetComponent<Weapon>().defaultWeapon.damage;
    }
}
