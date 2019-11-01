using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameScript : MonoBehaviour
{
    public enum renownTiers{Unknown, Viking, Ravager, Chieftain, Jarl, Sea_King };

    //Player's renown score
    public static float playerRenown;
    //Player's silver resource value
    public static float playerSilver;
    //duration is seconds of the enemy's attack
    public static float waveTime = 45;
    //hours per night the player may spend upgrading/repairing
    public static float nightHours;
    //if true, reset the game -- may not be needed
    public static bool gameOver;
    // if false, update waveTimer
    public static bool isNight;

    public int currentDay;

    [SerializeField]
    float waveLengthIncrease;
    [SerializeField]
    int waveStrIncrease;

    public bool isPaused;

    public static renownTiers tier;

    GameObject ws;
    GameObject player;

    static float currWaveTime;
    [SerializeField]
    Text skipDayText;
    
    void Awake()
    {
        
        currentDay = 0;
        currWaveTime = 0;
        playerRenown = 0;
        tier = renownTiers.Unknown;

        isNight = false;
        skipDayText.gameObject.SetActive(false);

        skipDayText.text = "Press [Enter] to rest until sunrise";

        ws = GameObject.FindGameObjectWithTag("Spawner");

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNight)
        {
            currWaveTime += Time.deltaTime;
            //Move Background 

            if (currWaveTime >= waveTime)
            {
                OnFinishWave();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartNextWave();
            }
        }

        HandlePause();
    }

    void OnFinishWave()
    {
        switch (playerRenown)
        {
            case float n when (n < 100):
                tier = renownTiers.Unknown;
                break;
            case float n when (n >= 100 && n < 500):
                tier = renownTiers.Viking;
                break;
            case float n when (n >= 500 && n < 1000):
                tier = renownTiers.Ravager;
                break;
            case float n when (n >= 1000 && n < 1500):
                tier = renownTiers.Chieftain;
                break;
            case float n when (n >= 1500 && n < 2500):
                tier = renownTiers.Jarl;
                break;
            case float n when (n >= 2500):
                tier = renownTiers.Sea_King;
                break;
        }
        skipDayText.gameObject.SetActive(true);
        isNight = true;
        currWaveTime = 0;
        waveTime += waveLengthIncrease;
        currentDay++;

        player.GetComponent<PlayerScript>().OnFinishWave();

    }

    void StartNextWave()
    {
        skipDayText.gameObject.SetActive(false);
        isNight = false;

        if (ws != null)
        ws.GetComponent<WaveSpawner>().UpdateSpawner(5);
    }

    void RestartGame()
    {
        waveTime = 45;

        currWaveTime = 0;
        playerRenown = 0;
        playerSilver = 0;

        isNight = false;

        if (ws != null)
            ws.GetComponent<WaveSpawner>().ResetSpawner();

        SceneManager.LoadScene("PlayScene");
    }

    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0.0f;
            //draw pause UI
        }
        else
        {
            Time.timeScale = 1.0f;
        }

    }

    public static float DayProgress()
    {
        return currWaveTime / waveTime;
    }

}
