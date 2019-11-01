using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private bool canAttack = true;

    [SerializeField]
    private float speed = 5;

    public float cutDamage = 10;
    public float bluntDamage = 0;

    public float knockback = 0;
    public float knockdown = 0;

    public float staminaDamage = 0;

    private Transform target;

    [SerializeField]
    private float attackTime = 0.2f;
    private float currentAttackTime;

    [SerializeField]
    private Transform attackPos;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask targets;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>() ?? GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Enemy Target").transform;

        currentAttackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentAttackTime -= Time.deltaTime;
        if (currentAttackTime <= 0)
        {
            canAttack = true;
            currentAttackTime = attackTime;
        }

        if (!MainGameScript.isNight)
        {
            if (target != null)
                rb.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            else
            {
                target = GameObject.FindGameObjectWithTag("Enemy Target").transform;
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Enemy Flee Target").transform;
            rb.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        //probably change this to a ray
        Collider2D[] attackees = Physics2D.OverlapCircleAll(attackPos.position, attackRange, targets);
        if (attackees.Length > 0)
        {
            anim.SetTrigger("AttackTrigger");
        }

        CheckFacing();
    }


    #region attack

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



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    #endregion

    void CheckFacing()
    {
        //use not using SpriteRenderer.flipX because I want child components to be transformed too

        //no else statement as if neither of these is true then we don't want to flip the character.

        if (!MainGameScript.isNight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (MainGameScript.isNight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
