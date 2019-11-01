using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgradeScript : MonoBehaviour
{
    [SerializeField]
    int currentLevel;
    [SerializeField]
    int maxLevel;

    [SerializeField]
    int upgradeCost;

    int defaultUpgradeCost;

    int totalSpent;

    [SerializeField]
    Canvas ui;

    [SerializeField]
    int upgradeCostIncrease;

    [SerializeField]
    GameObject[] upgrades;

    private bool isPlayerInRange;

    // Start is called before the first frame update
    void Awake()
    {

        defaultUpgradeCost = upgradeCost;

        currentLevel = 0;
        totalSpent = 0;

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
        if (currentLevel < maxLevel && isPlayerInRange)
        {
            if (MainGameScript.playerSilver >= upgradeCost && MainGameScript.isNight)
            {
                MainGameScript.playerSilver -= upgradeCost;
                totalSpent += upgradeCost;
                upgradeCost += upgradeCostIncrease;

                //disable current level's object, increase the value and enable the new object
                upgrades[currentLevel].SetActive(false);

                currentLevel++;

                upgrades[currentLevel].SetActive(true);
            }
        }
    }

    public void Sell()
    {
        if (currentLevel > 0 && isPlayerInRange)
        {
            MainGameScript.playerSilver += totalSpent;

            foreach (GameObject g in upgrades)
            {
                g.gameObject.SetActive(false);
            }

            upgradeCost = defaultUpgradeCost;
            totalSpent = 0;

            currentLevel = 0;
            upgrades[currentLevel].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && MainGameScript.isNight)
        {
            ui.gameObject.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ui.gameObject.SetActive(false);
            isPlayerInRange = false;
        }
    }

    public int GetLevel()
    {
        return currentLevel;
    }
}
