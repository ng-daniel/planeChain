using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontrolscript : MonoBehaviour
{
    public Animator transition;
    public float transitiontime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play()
    {

        loadScene(2);
    }
    public void mainmenu()
    {
        loadScene(1);
    }
    public void victory()
    {
        loadScene(3);
    }
    public void loadScene(int num)
    {
        StartCoroutine(LoadLevel(num));
    }
    
    IEnumerator LoadLevel(int levelindex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(levelindex);

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
