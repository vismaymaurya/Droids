using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform weaponHolder;
    public WeaponConfig defaultWeapon;
    public bool isAmmo;

    [HideInInspector]
    public WeaponConfig currentWeaponConfig = null;

    private void Awake()
    {
        currentWeaponConfig = defaultWeapon;
    }

    private void Start()
    {
       // WeaponSpwan(weaponHolder);
    }

    private void Update()
    {
        PlayerRotor();
    }

    public void WeaponSpwan(Transform weaponTransform)
    {
        currentWeaponConfig.SpawnWeapon(weaponTransform);
    }

    private void PlayerRotor()
    {
        Vector3 gunpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (gunpos.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ammo")
        {
            isAmmo = true;
            Debug.Log("Ammo");
            Destroy(collision.gameObject);
        }
    }
}
