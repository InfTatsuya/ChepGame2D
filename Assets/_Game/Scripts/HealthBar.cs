using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthFill;

    private float health;
    private float maxHealth;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, health / maxHealth, Time.deltaTime * 5f);
    }

    public void OnInit(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;

        healthFill.fillAmount = 1f;
    }

    public void SetNewHealth(float health)
    {
        this.health = health;
    }
}
