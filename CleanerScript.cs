using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Target"))
        {
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            //replace with MainGameScript.GameOver()
            Destroy(collision.gameObject);
        }
    }

}
