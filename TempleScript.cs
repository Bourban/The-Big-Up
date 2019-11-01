using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempleScript : MonoBehaviour
{
    [SerializeField]
    Canvas ui;

    private void Awake()
    {
        ui.gameObject.SetActive(false);
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

}