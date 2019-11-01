using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaveSpawner : MonoBehaviour
{
    public bool isActive = true;
    //value at which the more elite enemies mechanic activates -- see line 49 for more details
    [SerializeField]
    private float renownThreshold = 1500;
    //Create a static rng for all spawners -- no point having multiple object which all do the same thing
    static System.Random rnd = new System.Random();
    //publicly accessible for game designers to tweak timing
    public float waveDelay = 45.0f;
    [SerializeField]
    float defaultWaveStrength = 10;

    float waveStrength;
    //list of all enemy variants to be set in the editor
    public GameObject[] enemies;
    //value of each enemy type -- deducted from the current wave's total -- should keep waves balanced as long as the values are reasonable
    //values greater than the current max will not allow
    public float[] enemyValues;
    //counter to the next wave -- is reset to the value of waveDelay on creating a new wave
    private float currentWaveTime;
    

    void Update()
    {
        if (isActive)
        {
            currentWaveTime -= Time.deltaTime;
            if (currentWaveTime < 0)
            {
                CreateWave();
                currentWaveTime = waveDelay;
            }
        }

        //wave spawners only spawn enemies in the day!
        if (MainGameScript.isNight)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
    }
    private void Start()
    {
        currentWaveTime = 1;
        waveStrength = defaultWaveStrength;


    }
    void CreateWave()
    {
        float str = waveStrength;
        int i = 0, j = 0, k = 0;
        while (str > FindMinInArray())
        {
            i = rnd.Next(enemies.Length);

            //If player is over the renown threshold, gnenerate a second number and take the highest of the two
            //Should ensure more high tier units
            if (MainGameScript.playerRenown >= renownThreshold) {
                k = rnd.Next(enemies.Length);
                if (k > i)
                    i = k;
            }

            //prevents the script spawning any single enemy which has a larger point value than remaining strength
            //if the condition is not met the loop will simply iterate again and pick another index number
            if (enemyValues[i] <= str)
            {
                Instantiate(enemies[i], transform.position, Quaternion.identity);
                str -= enemyValues[i];

                j++;
                if (j > 100)
                    break;
            }
        }
    }
    float FindMinInArray()
    {
        float min = enemyValues[0];
        for (int i = 1; i < enemyValues.Length; i++)
        {
            if (enemyValues[i] < min)
                min = enemyValues[i];
        }
        return min;
    }

    public void ResetSpawner()
    {
        currentWaveTime = 1;
        waveStrength = defaultWaveStrength;
    }

    public void UpdateSpawner(int str)
    {
        waveStrength += str;
    }
}