using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaScript : MonoBehaviour
{
    public float maxStamina = 100;
    public float currentStamina;

    [Header("Regen value of stamina per second")]
    public float staminaRegen;

    public bool regainStamina;

    void Awake()
    {
        currentStamina = maxStamina;
        regainStamina = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (regainStamina) gainStamina();
    }

    public void staminaDrain(float staminaLoss)
    {
        currentStamina -= staminaLoss;

        if (currentStamina < 0)
        {
            regainStamina = true;
        }
    }

    public void gainStamina()
    {
        float _gain = staminaRegen / 60;
        currentStamina += _gain;
        if (currentStamina > maxStamina) currentStamina = maxStamina;
    }
}

