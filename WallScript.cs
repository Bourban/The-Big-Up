using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    HealthScript hs;

    [SerializeField]
    GameObject[] upgrades;

    [SerializeField]
    int currentLevel;
    [SerializeField]
    int maxLevel;

    [SerializeField]
    int upgradeCost;
    [SerializeField]
    int upgradeCostIncrease;

    [SerializeField]
    Canvas ui;

    void Awake()
    {
        currentLevel = 0;
        hs = upgrades[currentLevel].GetComponent<HealthScript>();  

        //disable all children (all upgrades levels) except the current one
        foreach (GameObject g in upgrades)
        {
            g.gameObject.SetActive(false);
        }

        upgrades[currentLevel].SetActive(true);

        ui.gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        if (currentLevel < maxLevel)
        {
            if (MainGameScript.playerSilver >= upgradeCost && MainGameScript.isNight)
            {
                MainGameScript.playerSilver -= upgradeCost;
                upgradeCost += upgradeCostIncrease;

                //disable current level's object, increase the value and enable the new object
                upgrades[currentLevel].SetActive(false);

                currentLevel++;

                upgrades[currentLevel].SetActive(true);
                hs = upgrades[currentLevel].GetComponent<HealthScript>();
            }
        }
    }

    //probably transfer theses to a generic upgrade/repair script
    public void Repair()
    {
        float cost = (hs.maxHealth - hs.currentHealth) / 10;

        Debug.Log(cost);
        if (MainGameScript.playerSilver > cost)
        {
            hs.FullHeal();
            MainGameScript.playerSilver -= cost;
        }
        Debug.Log(hs.currentHealth);
    }

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

    public int GetLevel()
    {
        return currentLevel;
    }

}
