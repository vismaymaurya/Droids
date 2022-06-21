using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    [HideInInspector]
    public bool useStamina = true;
    public float staminaRegenTime = 2f;

    public int maxStamina = 200;
    private int currentStamina;


    private WaitForSeconds regentTick = new WaitForSeconds(.01f);
    private Coroutine regen;

    public static StaminaBar instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;
            useStamina = true;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            useStamina = false;
            Debug.Log("Not enough stamna");
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(staminaRegenTime);

        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regentTick;
        }
        regen = null;
    }
}
