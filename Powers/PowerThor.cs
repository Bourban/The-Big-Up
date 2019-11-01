using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerThor : Power
{

    float frequency = 1.0f;
    float freqTime;

    public PowerThor()
    {
        duration = 5.0f;
        currTime = 0.0f;
    }

    public override void ActivatePower()
    {
        base.ActivatePower();

        isActive = true;
        //ensure that the first tick happens instantly
        freqTime = frequency;
    }

    public override string PowerType()
    {
        return "Thor";
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
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player Target");

            foreach (GameObject g in enemies)
            {
                Destroy(g);
            }

            freqTime = 0.0f;
        }

        if (currTime >= duration)
        {
            isActive = false;
            currTime = 0.0f;
        }
    }
}
