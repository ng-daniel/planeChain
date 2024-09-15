using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField] GameObject crash;
    [SerializeField] GameObject ranger;
    [SerializeField] GameObject cat;

    float nCrash;
    float nRanger;
    float nCat;

    List<float> PhaseTimes = new List<float>();
    [SerializeField] float waveInterval;
    bool isSpawning;
    [SerializeField] float phaseInterval;
    float phaseTimer;
    public int phase;

    [Header("Spawnboxes")]
    [SerializeField] Vector4 spawnBox1;

    // Start is called before the first frame update
    void Start()
    {
        nCrash = 1;
        nRanger = 0;
        nCat = 0;
        phase = 0;
        spawnWave();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(waveCoroutine());
        }

        switch (phase)
        {
            case 0:
                nCrash = 2;
                break;
            case 1:
                nCrash = 4;
                break;
            case 2:
                nCrash = 6;
                break;
            case 3:
                nCrash = 4;
                nRanger = 1;
                break;
            case 4:
                nCrash = 6;
                nRanger = 3;
                nCat = 1;
                break;
            case 5:
                nCrash = 6;
                nRanger = 5;
                nCat = 1;
                break;
            case 6:
                nCrash = 8;
                nRanger = 6;
                nCat = 1;
                break;
        }

        if (phaseTimer > 0)
        {
            phaseTimer -= Time.deltaTime;
        }
        else
        {
            phaseTimer = phaseInterval;
            if (phase < 5)
            {
                phase++;
            }
        }

    }

    IEnumerator waveCoroutine()
    {
        spawnWave();
        yield return new WaitForSeconds(waveInterval);
        isSpawning = false;
    }
    void spawnWave()
    {
        for (int i = 0; i < nCrash; i++)
        {
            spawnDrone("crash");
        }
        for (int i = 0; i < nRanger; i++)
        {
            spawnDrone("ranger");
        }
        for (int i = 0; i < nCat; i++)
        {
            spawnDrone("cat");
        }
    }
    void spawnDrone(String type)
    {
        switch (type)
        {
            case "crash":
                Instantiate(crash, randomPosition(), transform.rotation);
                break;
            case "ranger":
                Instantiate(ranger, randomPosition(), transform.rotation);
                break;
            case "cat":
                Instantiate(cat, randomPosition(), transform.rotation);
                break;
        }
    }
    Vector3 randomPosition()
    {
        float randX = UnityEngine.Random.Range(spawnBox1.x, spawnBox1.y);
        float randY = UnityEngine.Random.Range(spawnBox1.z, spawnBox1.w);
        return new Vector3(randX, randY, 0);
    }

}

/*
HOW THE SPAWNER SHOULD WORK

WAVES
- DRONES SPAWN IN WAVES
- EACH WAVE IS GIVEN SOME CREDITS TO SPEND
- IT WILL RANDOMLY PICK 

EVERY 30 SECONDS, THE PHASE WILL INCREASE BY ONE
0.  1-2 CRASHERS PER WAVE
1.  3-4 CRASHERS PER WAVE
2.  5-6 CRASHERS
3.  3-4 CRASHERS AND 1-2 RANGERS
4.  5-6 CRASHERS AND 3-4 RANGERS AND 1-2 SPAWNERS
5.  5-6 CRASHERS AND 3-4 RANGERS AND 3-4 SPAWNERS
THE GAME WILL PEAK AT PHASE 5
AFTER THAT IT WILL BECOME A GAME OF ATTRITION


*/
