using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flipper : MonoBehaviour

{

    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //use not using SpriteRenderer.flipX because I want child components to be transformed too
        //no else statement as if neither of these is true then we don't want to flip the character.
        if (rb.velocity.x > 0)
        {
            //isFacingLeft = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (rb.velocity.x < 0)
        {
            //isFacingLeft = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}

