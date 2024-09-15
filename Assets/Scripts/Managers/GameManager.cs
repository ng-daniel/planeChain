using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Game Manager is NULL.");
            }
            return _instance;
        }

    }

    [SerializeField] int score;
    [SerializeField] float time;
    [SerializeField] float speed;
    [SerializeField] float height;

    planeScript playerScript;

    public bool isDead;

    public AudioSource metalPipe;


    void Start()
    {
        _instance = this;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            if (!isDead)
            {
                isDead = true;
                metalPipe.Play();
            }

        }
        else
        {
            playerScript = player.GetComponent<planeScript>();
        }

    }
    void Update()
    {
        if (playerScript == null)
        {
            isDead = true;
            speed = 0;
        }

        if (isDead)
        {

            return;
        }
        time += Time.deltaTime;
        speed = playerScript.getSpeed();
        height = playerScript.getHeight();
    }
    public void incrementScore(int num)
    {
        score += num * 100;
    }
    public int getScore()
    {
        return score;
    }
    public float getTime()
    {
        return time;
    }
    public float getSpeed()
    {
        return speed;
    }
    public float getHeight()
    {
        return height;
    }


}
