using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    private float currenthealth;

    public HealthBar healthBar = null;

    public static Health instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnEnable()
    {
        currenthealth = maxHealth;
    }

    public void TakeDamge(float damageAmount)
    {
        currenthealth -= damageAmount;
        healthBar.SetHealth(currenthealth);

        if (currenthealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GetComponent<EnemyCtrl>().Drop();
    }

    public void IncreaseHealth(float level)
    {
        maxHealth += (currenthealth * .01f) * ((100 - level) * .1f);
        currenthealth = maxHealth;
        healthBar.SetMaxHealth(currenthealth);
    }
}
