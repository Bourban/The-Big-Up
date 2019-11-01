using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{

    public Transform projectile;
    Transform spawner;
    [Header("In seconds")]
    public float fireRate;
    public float range;
    public bool holdFire;
    public bool fireForward;
    [Header("Read only!")]
    public Transform target;
    public Vector3 targetOffset;

    void Awake()
    {
        spawner = transform.Find("spawner");
        InvokeRepeating("LaunchProjectile", fireRate, fireRate);
    }

    void LaunchProjectile()
    {
        if (holdFire) return;

        try
        {
            target = GameObject.FindGameObjectWithTag("Player Target").transform;
        }
        catch
        {
            target = null;
        }
        Vector2 dirToTarget = new Vector2(0, 0);
        if (target != null)
        {
            dirToTarget = ((target.position + targetOffset) - spawner.position).normalized;
        }
        else if (!fireForward)
        {
            return; //If No target found and we dont have the fireForward mode set just return
        }

        if (Vector3.Distance(spawner.position, target.position) > range)
        {
            return;
        }

        Transform newProjectile = Instantiate(projectile, spawner.position, spawner.transform.rotation);
        Rigidbody2D rb;
        rb = newProjectile.gameObject.GetComponent<Rigidbody2D>();
        if (fireForward)
        {
            rb.AddForce(newProjectile.up * 100f);
        }
        else
        {
            rb.AddForce(dirToTarget * 100f);
            spawner.up = -(newProjectile.position - (target.position + targetOffset));
            newProjectile.up = -(newProjectile.position - (target.position + targetOffset));
        }
    }
}
