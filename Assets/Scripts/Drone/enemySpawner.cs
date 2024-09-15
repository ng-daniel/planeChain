using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField] GameObject crash;
    [SerializeField] GameObject ranger;
    [SerializeField] GameObject cat;


    List<float> PhaseTimes = new List<float>();
    float waveInterval;

    public enum Phase
    {
        ZERO,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE
    }
    public Phase phase;

    // Start is called before the first frame update
    void Start()
    {
        phase = Phase.ZERO;
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case Phase.ZERO:
                break;
            case Phase.ONE:
                break;
            case Phase.TWO:
                break;
            case Phase.THREE:
                break;
            case Phase.FOUR:
                break;
            case Phase.FIVE:
                break;

        }
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
