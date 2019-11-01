using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDamageScript : MonoBehaviour
{

    [Header("Light Attack")]
    [SerializeField]
    float cutDamage;
    [SerializeField]
    float bluntDamage;
    [Header("Heavy Attack")]
    [SerializeField]
    float heavyCutDam;
    [SerializeField]
    float heavyBluntDam;

    [SerializeField]
    float damMultiplier = 1.0f;

    [SerializeField]
    Transform onHitParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Target"))
        {
            //if anim state == light attack etc.

            collision.GetComponent<HealthScript>().takeDamage(cutDamage * damMultiplier, bluntDamage * damMultiplier);
            Instantiate(onHitParticles, collision.transform.position, Quaternion.identity);
        }
    }

    public void SetDamageMult(float newMult)
    {
        damMultiplier = newMult;
    }
}
