using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
public class WeaponConfig : ScriptableObject
{
    enum Weapons
    {
        NormalShooter,
        PlasmaGun,
        ExplosiveGun,
        ElectroGun,
        FlameThrower,
        LaserGun,
        RocketLauncher
    }

    enum TypeOfWeapon
    {
        SingleShot,
        Automatic,
        SemiAutomatic,
        Burst,
    }

    [SerializeField] Weapons weapons;
    [SerializeField] TypeOfWeapon weaponType;
    [Tooltip("Weapon Icon")]
    public Sprite Icon;
    [Tooltip("Weapon Bullet/Projectile Fire Speed")]
    public float fireRate = .05f;
    [Tooltip("How much time Req. to reload Weapon")]
    public float reloadTime = 5;
    [Tooltip("Area of explosion for explosive type weapon only")]
    public float explosionArea;
    [Tooltip("Damage Done by projectile/Bullet")]
    public float damage = 10;
    [Tooltip("Max Amount of ammo hold by weapon")]
    public int maxAmmoAmount = 100;
    [Tooltip("Amount of ammo hold by weapon")]
    public int minAmmoAmount = 30;
    [Tooltip("Number of bullets/projeectile fire in burst")]
    public float burstAmmount = 3;
    [Tooltip("Do not touch this value" + " as it derived from weaponType value")]
    public int weaponTypeArray;
    [Tooltip("Projectile/Bullets")]
    public ProjectileFire projectile;
    public WeaponCtrl equippedPrefab = null;

    const string weaponName = "Weapon";

    public WeaponCtrl SpawnWeapon(Transform weaponTransform)
    {
        DestroyOldWeapon(weaponTransform);

        WeaponCtrl weapon = null;

        if (equippedPrefab != null)
        {
            Transform handTransform = GetTransform(weaponTransform);
            weapon = Instantiate(equippedPrefab, handTransform);
            weapon.gameObject.name = weaponName;
        }

        return weapon;

    }

    public void DestroyOldWeapon(Transform weaponTransform)
    {
        Transform oldWeapon = weaponTransform.Find(weaponName);

        if (oldWeapon == null) { return; }
        oldWeapon.name = "Destroying";
        Destroy(oldWeapon.gameObject);
    }

    public void GetWeaponTypeValue()
    {
        if (weaponType == TypeOfWeapon.SingleShot)
        {
            weaponTypeArray = 1;
        }
        else if (weaponType == TypeOfWeapon.Automatic)
        {
            weaponTypeArray = 2;
        }
        else if (weaponType == TypeOfWeapon.SemiAutomatic)
        {
            weaponTypeArray = 3;
        }
        else
        {
            weaponTypeArray = 4;
        }
    }

    private Transform GetTransform(Transform weaponTransform)
    {
        Transform handTransform;
        handTransform = weaponTransform;
        return handTransform;
    }
}
