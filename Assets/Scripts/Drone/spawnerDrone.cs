using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class spawnerDrone : MonoBehaviour
{


    Rigidbody2D rb;
    [SerializeField] float speed;


    [SerializeField] float rangeRadius;
    bool detectingPlayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 playerPos;

    [Header("SpawnVars")]
    float spawnTimer;
    [SerializeField] float spawnTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        detectingPlayer = Physics2D.OverlapCircle(transform.position, rangeRadius, playerLayer);
        if (detectingPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerPos = player.transform.position;

        }
        else
        {
            playerPos = transform.position;
        }

        Vector3 direction = (playerPos - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;

    }
}
