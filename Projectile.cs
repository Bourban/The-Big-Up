using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float damageBlunt;
    [SerializeField]
    float damageCut;
    [SerializeField]
    float knockback;

    float mass = 0.1f;

    Rigidbody2D rb;
    public Transform bloodParticles;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, 300 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       Transform otherT = collision.transform;
       if (otherT.gameObject.tag == "Player Target")
       {
            HealthScript health = otherT.gameObject.GetComponent<HealthScript>();
            health.takeDamage(damageBlunt, damageCut, knockback, this.transform.right); 
            Instantiate(bloodParticles, otherT.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
       
    }
}
