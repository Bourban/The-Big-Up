using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{

    public Text renownText;
    public Text silverText;

    // Update is called once per frame
    void Update()
    {
        renownText.text = MainGameScript.tier.ToString();
        silverText.text = MainGameScript.playerSilver.ToString();
    }
}
