using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public GameObject player;
    public string weaponName = "Weapon_1";
    public Button itemBtn;
    public Image itemImg;
    public Image itemTransparentImg;

    private WeaponConfig weaponConfig;

    private void Start()
    {
        itemBtn.interactable = false;
    }

    private void Update()
    {
        if (weaponName == player.GetComponent<WeaponChecker>().weaponName)
        {
            itemBtn.interactable = true;
            itemImg.sprite = player.GetComponent<WeaponChecker>().weaponIcon;
            weaponConfig = player.GetComponent<WeaponChecker>().weapon;
        }

        if (itemBtn.interactable)
        {
            itemTransparentImg.gameObject.SetActive(false);
        }
        else
        {
            itemTransparentImg.gameObject.SetActive(true);
        }
    }

    public void SwitchWeapon()
    {
        player.GetComponent<Weapon>().currentWeaponConfig = weaponConfig;
        player.GetComponent<Weapon>().WeaponSpwan(player.transform);
    }
}
