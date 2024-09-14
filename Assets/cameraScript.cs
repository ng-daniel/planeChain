using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{

    public Vector2 xBounds;
    public Vector2 yBounds;

    public Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }
        Vector3 playerPos = player.transform.position;

        float playerX = playerPos.x;
        float playerY = playerPos.y;

        float cameraX = playerX;
        float cameraY = playerY;
        if (playerX < xBounds.x)
        {
            cameraX = xBounds.x;
        }
        if (playerX > xBounds.y)
        {
            cameraX = xBounds.y;
        }
        if (playerY < yBounds.x)
        {
            cameraY = yBounds.x;
        }
        if (playerY > yBounds.y)
        {
            cameraY = yBounds.y;
        }

        Vector3 targetPosition = new Vector3(cameraX, cameraY, transform.position.z);
        Vector3 lerpPosition = Vector3.Lerp(transform.position, targetPosition, 0.5f);

        transform.position = lerpPosition;


    }
}
