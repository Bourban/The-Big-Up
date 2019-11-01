using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField]
    float cutDamage;
    [SerializeField]
    float bluntDamage;
    [SerializeField]
    float staminaDamage;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStaminaScript ps = other.GetComponent<PlayerStaminaScript>();
            if (!ps.regainStamina)
            {
                Debug.Log("taking stamina damage");
                ps.staminaDrain(staminaDamage);
            }

            else
            {
                HealthScript h = other.GetComponent<HealthScript>() ?? other.GetComponentInParent<HealthScript>();
                if (h != null)
                    h.takeDamage(cutDamage, bluntDamage);
            }
        }
        else if (other.CompareTag("Enemy Target"))
        {
            HealthScript h = other.GetComponent<HealthScript>() ?? other.GetComponentInParent<HealthScript>();
            if (h != null)
                h.takeDamage(cutDamage, bluntDamage);
        }
    }
}
