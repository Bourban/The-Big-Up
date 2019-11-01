using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithScript : MonoBehaviour
{
    [SerializeField]
    float weapUpgradeCost = 500;
    [SerializeField]
    float upgradedWeapDamMult = 1.2f;
    [SerializeField]
    int maxAmmoIncrease = 3;
    [SerializeField]
    float ammoCapUpCost = 500;
    [Header("this is a multiplier")]
    [SerializeField]
    float upgradeCostIncrease = 2;

    [SerializeField]
    Canvas ui;

    GameObject player;

    bool hasUpgradedWeapons;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found");
        }

        hasUpgradedWeapons = false;

        ui.gameObject.SetActive(false);
    }

    public void RefillAmmo()
    {
        player.GetComponent<RangedAttack>().RefillAmmo();
    }

    public void IncreaseAmmoCap()
    {
        if (MainGameScript.playerSilver > ammoCapUpCost)
        {
            MainGameScript.playerSilver -= ammoCapUpCost;
            player.GetComponent<RangedAttack>().ModifyMaxAmmo(maxAmmoIncrease);

            ammoCapUpCost *= upgradeCostIncrease;
        }
        else
        {
            Debug.Log("Not enough dosh pal");
        }
    }

    public void WeaponUpgrade()
    {
        if (!hasUpgradedWeapons && MainGameScript.playerSilver > weapUpgradeCost)
        {
            MainGameScript.playerSilver -= weapUpgradeCost;
            player.GetComponentInChildren<PlayerDamageScript>().SetDamageMult(upgradedWeapDamMult);
            hasUpgradedWeapons = true;
        }
        else
        {
            Debug.Log("Not enough dosh pal");
        }

    }

    #region UI Toggle

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && MainGameScript.isNight)
        {
            ui.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ui.gameObject.SetActive(false);
        }
    }
    #endregion
}
