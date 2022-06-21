using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;
    [Header("UI")]
    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI maxAmmoText;
    public TextMeshProUGUI reloadTimerText;
    public Image reloadImage;

    private float timeBtwShots;
    private float startTimeBtwShots;
    private float burstAmmount;
    private float timeBtwBurst;
    private float currentAmmo;
    private float minAmmo;
    private float maxAmmo = 100;
    private float reloadTime;
    private float currentTime;

    private bool isReloading;
    private bool ammo;

    private Weapon weapon;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weapon = player.GetComponent<Weapon>();
        startTimeBtwShots = weapon.currentWeaponConfig.fireRate;
        burstAmmount = weapon.currentWeaponConfig.burstAmmount;
        timeBtwShots = startTimeBtwShots;
        timeBtwBurst = burstAmmount;
        maxAmmo = weapon.currentWeaponConfig.maxAmmoAmount;
        minAmmo = weapon.currentWeaponConfig.minAmmoAmount;
        currentAmmo = minAmmo;
        reloadTime = weapon.currentWeaponConfig.reloadTime;
        reloadImage.fillAmount = currentTime / reloadTime;
    }

    private void Update()
    {
        currentAmmoText.text = "" + currentAmmo;
        maxAmmoText.text = "/ " + maxAmmo;

        ammo = weapon.isAmmo;
        weapon.currentWeaponConfig.GetWeaponTypeValue();
        FillAmmo();

        if (isReloading || minAmmo == 0)
        {
            if (isReloading)
            {
                reloadImage.gameObject.SetActive(true);
                if (currentTime >= reloadTime)
                {
                    currentTime = 0;
                }
                else
                {
                    reloadTimerText.text = "" + Mathf.Round(currentTime * 10f) * .1f;
                    reloadImage.fillAmount = currentTime / reloadTime;
                    currentTime += Time.deltaTime;
                }
            }
            return;
        }

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (maxAmmo < minAmmo)
            {
                minAmmo = maxAmmo;
            }
            StartCoroutine(Reload());
            return;
        }
        WeaponSelection();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);
        ReloadUI();

        maxAmmo -= minAmmo - currentAmmo;
        currentAmmo = minAmmo;

        isReloading = false;
    }

    private void FillAmmo()
    {
        if (ammo)
        {
            Debug.Log("AmmoGained");
            maxAmmo += 30;
            weapon.isAmmo = false;
        }

        if (maxAmmo <= 0)
        {
            isReloading = false;
        }
        else if (maxAmmo > minAmmo && maxAmmo <= weapon.currentWeaponConfig.minAmmoAmount)
        {
            minAmmo = maxAmmo;
        }
    }

    private void ReloadUI()
    {
        reloadImage.gameObject.SetActive(false);
        Debug.Log("ReloadCOmplete");
    }

    private void WeaponSelection()
    {
        var WeaponType = weapon.currentWeaponConfig.weaponTypeArray;

        if (WeaponType == 1)
        {
            SingleShot();
        }
        else if (WeaponType == 2)
        {
            AutomaticShot();
        }
        else if (WeaponType == 3)
        {
            SemiAutomaticShot();
        }
        else
        {
            BurstShot();
        }
    }

    private void SingleShot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }
    }

    private void AutomaticShot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timeBtwShots >= startTimeBtwShots)
            {
                Shooting();
                timeBtwShots = 0;
            }
            else
            {
                timeBtwShots += Time.deltaTime;
            }
        }
    }

    private void SemiAutomaticShot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timeBtwShots >= (startTimeBtwShots + .1f))
            {
                Shooting();
                timeBtwShots = 0;
            }
            else
            {
                timeBtwShots += Time.deltaTime;
            }
        }
    }

    private void BurstShot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (timeBtwBurst >= (burstAmmount / 5))
            {
                Shooting();
                Invoke("Shooting", burstAmmount / 40);
                Invoke("Shooting", burstAmmount / 20);

                timeBtwBurst = 0;
            }
        }
        else
        {
            timeBtwBurst += Time.deltaTime;
        }
    }

    private void Shooting()
    {
        currentAmmo--;

        GameObject projectile = weapon.currentWeaponConfig.projectile.gameObject;
        GameObject shoot = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Destroy(shoot, 2f);
    }
}
