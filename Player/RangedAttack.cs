using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RangedAttack : MonoBehaviour
{
    [SerializeField]
    int maxAmmo;
    int currentAmmo;

    [SerializeField]
    float fireRate;
    float fireTimer;

    [SerializeField]
    float projectileForce;

    bool canFire;

    [SerializeField]
    Transform projectile;
    [SerializeField]
    Transform attackPos;

    [SerializeField]
    TextMeshProUGUI ammoCounter;

    private void Awake()
    {
        canFire = true;
        currentAmmo = maxAmmo;

        ammoCounter.text = currentAmmo.ToString() + '/' + maxAmmo.ToString();
    }

    private void Update()
    {
        if (!canFire)
        {
            fireTimer += Time.deltaTime;

            if (fireTimer > fireRate)
            {
                fireTimer = 0;
                canFire = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (canFire && currentAmmo > 0)
        {
            Transform newProjectile = Instantiate(projectile, attackPos.position, Quaternion.identity);
            if (transform.eulerAngles.y == 0)
            {
                newProjectile.eulerAngles = new Vector3(newProjectile.eulerAngles.x, newProjectile.eulerAngles.y, -80);
            }
            else
            {
                newProjectile.eulerAngles = new Vector3(newProjectile.eulerAngles.x, newProjectile.eulerAngles.y, 80);
            }
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            rb.AddForce(newProjectile.up * projectileForce);

            currentAmmo--;

            ammoCounter.text = currentAmmo.ToString() + '/' + maxAmmo.ToString();

            canFire = false;
        }
    }

    public void ModifyMaxAmmo(int change)
    {
        maxAmmo += change;
        ammoCounter.text = currentAmmo.ToString() + '/' + maxAmmo.ToString();
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
        ammoCounter.text = currentAmmo.ToString() + '/' + maxAmmo.ToString();
    }
}
