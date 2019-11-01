using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthScript : MonoBehaviour
{
    //Make sure to only set this for Enemies -- Or not, player could get some silver back from destroyed buildings?
    public float silverValue = 0;
    [SerializeField]
    float repValue;

    public float maxHealth = 100;
    public bool hasDamageNumbers;

    [SerializeField]
    private float cutArmour = 20;
    [SerializeField]
    private float bluntArmour = 5;

    public float knockbackRes = 1;
    public float knockdownRes = 1;

    public float currentHealth;

    private Rigidbody2D rb;

    public bool invulnerable;
    public bool isPlayer;

    public Transform audioPrefab;
    public AudioClip coins;

    public Transform onDeathEffect;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (currentHealth <= 0 && !gameObject.GetComponent<PlayerScript>().GetIsDead())
            {
                Debug.Log("pls");
                gameObject.GetComponent<PlayerScript>().Die();
            }

        }

        else if (currentHealth <= 0)
        {
            MainGameScript.playerSilver += silverValue;
            MainGameScript.playerRenown += repValue;

            //Transform _newAudio = Instantiate(audioPrefab, this.transform.position, Quaternion.identity);
            //AudioSource _newSource = _newAudio.GetComponent<AudioSource>();
            //_newSource.clip = coins;
            //_newSource.Play();

            if (onDeathEffect != null)
            {
                Instantiate(onDeathEffect, this.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    }

    public void takeDamage(float cutDam, float bluntDam)
    {
        if (invulnerable) return;

        float damage = 0;

        damage += (cutDam / 100) * cutArmour;
        if (bluntDam - bluntArmour > 0)
            damage += bluntDam - bluntArmour;

        currentHealth -= damage;

        Animator anim = gameObject.GetComponent<Animator>() ?? GetComponentInChildren<Animator>();
        if (anim)
        {
            anim.SetTrigger("BeenHit");
        }
    }

    public void takeDamage(float cutDam, float bluntDam, float knockback, Vector2 dir)
    {
        if (invulnerable) return;

        float damage = 0;
        damage += (cutDam / 100) * cutArmour;
        if (bluntDam - bluntArmour > 0)
            damage += bluntDam - bluntArmour;

        currentHealth -= damage;
        //Debug.Log(gameObject + " has taken " + damage + " damage");

        float kb = knockback - knockbackRes;

        if (kb > 0)
        {
            applyKnockback(kb, dir);
            Debug.Log(kb + " knockback applied");
        }
    }

    void applyKnockback(float knockback, Vector2 dir)
    {
        if (rb != null)
            //pretty sure setting the player's velocity manually for movement is messing this up -- should work on enemies
            rb.AddForce(dir * knockback * (-1));
    }


    //heal this
    public void gainHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }
}
