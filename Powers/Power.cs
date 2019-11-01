using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    protected float duration = 0.0f;
    protected float currTime;

    [SerializeField]
    protected bool isActive;

    public Power()
    {

    }

    public virtual void ActivatePower()
    {
        //isActive = true;
    }

    public virtual string PowerType()
    {
        return "generic power";
    }

    public virtual void UpdatePower()
    {
        //    if (!isActive)
        //        return;

        //    //keeps track of time since ActivatePower was called
        //    currTime += Time.deltaTime;

        //    //once currTime is greater than duration, set isActive to false to stop updating
        //    //powers with a duration of 0 should still run this function in its entirety (including overrides) once
        //if (currTime >= duration)
        //{
        //    isActive = false;
        //    currTime = 0.0f;
        //}
    }
}
