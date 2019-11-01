using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    GameObject player;
    [Header("as type use 'health' or 'stamina'")]
    public string typeOfBar;         //Added stamina
    Image bar;
    Color originalColor;



    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bar = GetComponentInChildren<Image>();
        originalColor = bar.color;
    }

    // Update is called once per frame
    void Update()
    {
        float _change = 0;

        switch (typeOfBar)
        {
            case "health":
                _change = player.GetComponent<HealthScript>().currentHealth / player.GetComponent<HealthScript>().maxHealth;
                break;

            case "stamina":
                _change = player.GetComponent<PlayerStaminaScript>().currentStamina / player.GetComponent<PlayerStaminaScript>().maxStamina;
                break;

            case "default":
                Debug.LogError("Wrong bar type given");
                break;
        }

       gameObject.transform.localScale = new Vector3(_change, 1, 1);
        if (gameObject.transform.localScale.x < 0.05f)
            gameObject.transform.localScale = new Vector3(0.05f, 1, 1);
    }
}
