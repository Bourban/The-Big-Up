using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    //need to merge this into player script

    bool bIsTouchingFloor;

    public float fJumpHeight = 50;
    public float fFeetRadius = 0.5f;

    public Transform feetPos;
    public LayerMask groundLayer;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        bIsTouchingFloor = Physics2D.OverlapCircle(feetPos.position, fFeetRadius, groundLayer);

        
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

    }

    void Jump()
    {
        if (bIsTouchingFloor)
        {
            rb.velocity = Vector2.up * fJumpHeight;
        }
    }

}

