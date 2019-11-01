using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    bool isTouchingFloor;
    bool isHealing;
    bool canUsePower;
    bool hasPower;

    bool isDead;

    PlayerStaminaScript stam;
    HealthScript hs;

    [SerializeField]
    float speed = 5;

    [SerializeField]
    float healRate = 10;

    Power power;

    [SerializeField]
    Text powerText;

    float movementDir;


    void Awake()
    {
        stam = GetComponent<PlayerStaminaScript>();
        hs = GetComponent<HealthScript>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        anim.SetBool("IsDead", false);

        canUsePower = false;
        hasPower = false;
        isDead = false;

    }

    private void Update()
    {
        if (!isDead)
        {
            //unity auto designates A and D as horizontal axis -- should work for controller
            movementDir = Input.GetAxisRaw("Horizontal");

            //disable movement while blocking or healing
            if (!isHealing && !anim.GetBool("IsBlocking"))
            {
                //set object's velocity.x to move speed * input value -- should allow analogue controls to move more slowly
                //keeping the current y velocity by just assigning it its current value
                rb.velocity = new Vector2(movementDir * speed, rb.velocity.y);
            }

            if (isHealing)
            {
                hs.gainHealth(healRate * Time.deltaTime);
            }

            CheckFacing();

            //isTouchingFloor = Physics2D.OverlapCircle(feetPos.position, feetRadius, groundLayer);

            HandleInput();

            if (power)
            {
                power.UpdatePower();
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isHealing = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHealing = false;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (power && canUsePower) //prevent calling if uninitialized
            {
                power.ActivatePower();
                canUsePower = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetTrigger("LightAttack");
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            anim.SetTrigger("HeavyAttack");
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            anim.SetTrigger("Flurry");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("IsBlocking", true);
            stam.regainStamina = false;

        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("IsBlocking", false);
            stam.regainStamina = true;
        }

        if (movementDir != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void CheckFacing()
    {
        //not using SpriteRenderer.flipX because I want child components to be transformed too

        //no else statement as if neither of these is true then we don't want to flip the character.

        //using input direction so that the player won't get turned arnoud because of knockback

        if (movementDir > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (movementDir < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public bool GetIsHealing()
    {
        return isHealing;
    }

    public void SetPower(string choice)
    {
        if (!hasPower)
        {

            Debug.Log("Setting power");
            switch (choice)
            {
                case "Thor":
                    power = gameObject.AddComponent<PowerThor>();
                    canUsePower = true;

                    break;
                case "Odin":
                    power = gameObject.AddComponent<PowerOdin>();
                    canUsePower = true;
                    break;
                case "Freja":
                    power = gameObject.AddComponent<PowerFreja>();
                    canUsePower = true;
                    break;
                default:
                    Debug.Log("string is not a power");
                    break;
            }
        }
    }

    //called at the end of every day -- restores daily power use etc;
    public void OnFinishWave()
    {
        canUsePower = true;
    }

    public void Die()
    {
        anim.SetBool("IsDead", true);
        anim.SetTrigger("Die");
  
        isDead = true;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public bool GetHasPower()
    {
        return hasPower;
    }
}