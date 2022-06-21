using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXp;
    public float requiredXp;

    private float lerpTimer;
    private float delayTimer;

    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI txtLevel;

    [Header("RequiredXpVariables")]
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f, 8f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;

    private void Start()
    {
        frontXpBar.fillAmount = currentXp / requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;

        requiredXp = CalculateRequiredXp();

        txtLevel.text = "" + level;
    }

    private void Update()
    {
        UpdateXpUI();

        if (currentXp > requiredXp)
        {
            LevelUp();
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float Fxp = frontXpBar.fillAmount;

        if (Fxp < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentageComplete = lerpTimer / 4;
                frontXpBar.fillAmount = Mathf.Lerp(Fxp, backXpBar.fillAmount, percentageComplete);
            }
        }
    }

    public void GainExperienceFlatRate(float xpGained)
    {
        currentXp += xpGained;
        lerpTimer = 0;
        delayTimer = 0;
    }

    public void LevelUp()
    {
        level++;
        frontXpBar.fillAmount = 0;
        backXpBar.fillAmount = 0;

        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        GetComponent<Health>().IncreaseHealth(level);
        requiredXp = CalculateRequiredXp();
        txtLevel.text = "" + level;
    }

    private int CalculateRequiredXp()
    {
        int solveForRequireXp = 0;

        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequireXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequireXp / 4;
    }
}
