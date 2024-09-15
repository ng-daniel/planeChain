using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isDead && Input.GetKeyDown(KeyCode.R))
        {
            SceneManagerScript.Instance.LoadScene(1);
        }
    }
}
