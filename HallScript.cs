using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallScript : MonoBehaviour
{

    [SerializeField]
    int nightHealthRecovery;
    [SerializeField]
    int dailySilver;
    [SerializeField]
    int dailyRep;

    GameObject player;

    BuildingUpgradeScript bs;

    private void Awake()
    {
        bs = GetComponent<BuildingUpgradeScript>();
    }

    public void OnDayEnd()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<HealthScript>().gainHealth(nightHealthRecovery);
            
        CalculateDailyRep();
        CalculateDailySilver();

        MainGameScript.playerSilver += dailySilver;
        MainGameScript.playerRenown += dailyRep;

    }

    void CalculateDailySilver()
    {
        dailySilver *= bs.GetLevel();
    }

    void CalculateDailyRep()
    {
        dailyRep *= bs.GetLevel();
    }

}
