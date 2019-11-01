using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public bool isPlayer;
    public float maxStamina = 100;
    public float currentStamina;
    [Header("Min value of stamina required to block")]
    public float blockCost;
    [Header("Regen value of stamina per second")]
    public float staminaRegen;
    public bool regainStamina;
    CharacterController player;
    void Awake()
    {
        currentStamina = maxStamina;
        player = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (regainStamina) gainStamina();
    }


    public void staminaDrain(float staminaLoss)
    {
        currentStamina -= staminaLoss;
        if (currentStamina < 0) currentStamina = 0;
    }

    public void gainStamina()
    {
        float _gain = staminaRegen / 60;
        currentStamina += _gain;
        if (currentStamina > maxStamina) currentStamina = maxStamina;
    }


}
