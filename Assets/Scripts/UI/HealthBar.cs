using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    [SerializeField] private bool castle;
    // Update is called once per frame
    void Update()
    {
        if (!castle)
            transform.rotation = Quaternion.Euler(0, 0, 0); 
        
    }

    public void HealthBarUpdate(float maxHealth, float currentHealth)
    {
        print("Max =" + maxHealth +" current = " +currentHealth);
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}