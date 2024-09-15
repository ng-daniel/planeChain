using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private static SceneManagerScript _instance;
    public static SceneManagerScript Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Scene Manager is NULL.");
            }
            return _instance;
        }

    }
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }
    public void resetGame()
    {
        LoadScene(0);
    }
}
