using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFreja : Power
{

    float frequency = 0.1f;
    float freqTime;

    public PowerFreja()
    {
        duration = 10.0f;
    }

    GameObject player;

    public override void ActivatePower()
    {
        base.ActivatePower();

        isActive = true;
        freqTime = frequency;

        player = GameObject.FindGameObjectWithTag("Player");

       
    }

    public override string PowerType()
    {
        return "Freja";
    }

    public override void UpdatePower()
    {
        base.UpdatePower();

        if (!isActive)
            return;

        freqTime += Time.deltaTime;
        currTime += Time.deltaTime;

        //Actual power thing happens here -- this just deletes all enemies every frequency seconds
        if (freqTime >= frequency)
        {
            player.GetComponent<HealthScript>().gainHealth(5);


            freqTime = 0.0f;
        }

        if (currTime >= duration)
        {
            isActive = false;
            currTime = 0.0f;
        }
    }

}

