using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{


    public GameObject playScreen;
    public GameObject gameOverScreen;

    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text speedText;
    public TMP_Text heightText;

    public TMP_Text overScore;
    public TMP_Text overTime;

    int score;
    float time;
    float speed;
    float height;

    public List<GameObject> phaseIcons = new List<GameObject>();
    int phaseNum;

    // Start is called before the first frame update
    void Start()
    {
        playScreen.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.isDead)
        {
            if (playScreen.activeSelf)
            {
                playScreen.SetActive(false);
            }
            if (!gameOverScreen.activeSelf)
            {
                gameOverScreen.SetActive(true);
            }
            overScore.text = formatScore(score);
            overTime.text = formatTime(time);
            return;
        }

        score = GameManager.Instance.getScore();
        time = GameManager.Instance.getTime();
        speed = GameManager.Instance.getSpeed();
        height = GameManager.Instance.getHeight();

        scoreText.text = formatScore(score);
        timeText.text = formatTime(time);
        speedText.text = ((int)(speed * 17f)).ToString();
        heightText.text = ((int)(height * 100f) + 765).ToString();
    }
    string formatScore(float score)
    {
        int numChars = 7;
        string str = score.ToString();
        for (int i = str.Length; i < numChars; i++)
        {
            str = "0" + str;
        }
        return str;
    }
    string formatTime(float time)
    {
        int timeInt = (int)time;
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;

        string zero = "";
        if (seconds < 10)
        {
            zero = "0";
        }

        string str = minutes + ":" + zero + seconds;
        return str;
    }
}
