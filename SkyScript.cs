using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScript : MonoBehaviour
{
    //initializing these to our default, just in case they're not set in the editor
    [SerializeField]
    float startYPos = -90;
    [SerializeField]
    float endYPos = 99;

    private Vector3 pos1;
    private Vector3 pos2;

    // Start is called before the first frame update
    void Start()
    {
        //setting up two positions for the texture to move between, since it's only moving on the Y axis 
        //I'm setting the X and Z values to what they already are, ensuring they won't be affected
        pos1 = new Vector3(transform.position.x, startYPos, transform.position.z);
        pos2 = new Vector3(transform.position.x, endYPos, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Night doesn't last a determinate amount of time, so only move the texture during the day
        if (!MainGameScript.isNight)
        {
            //Lerp (linearly interpolate) between our two positions using a percentage of the day's progress as a weight, meaning this will automatically
            //adjust to changes in the day's length.
            transform.position = Vector3.Lerp(pos1, pos2, MainGameScript.DayProgress());
        }
        else
        {
            //the bottom of the texture is identical to the top so this may seem simple but it creates a smooth transition
            transform.position = pos2;
        }  
    }
}
