using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOdin : Power
{

    public PowerOdin()
    {
        duration = 15.0f;
    }

    public override void ActivatePower()
    {
        base.ActivatePower();
        Debug.Log("Odin Power");
    }

    public override string PowerType()
    {
        return "Odin";
    }


    public override void UpdatePower()
    {
        base.UpdatePower();

        if (!isActive)
            return;

        currTime += Time.deltaTime;

        //do stuffs here

        if (currTime >= duration)
        {
            isActive = false;
            currTime = 0.0f;
        }
    }
}
