using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class spawnerDrone : MonoBehaviour
{


    Rigidbody2D rb;
    [SerializeField] float speed;
    public GameObject bomber;


    [SerializeField] float rangeRadius;
    bool detectingPlayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 playerPos;

    [Header("SpawnVars")]
    [SerializeField] float spawnTime;
    bool isSpawning;

    SpriteRenderer sRender;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


        detectingPlayer = Physics2D.OverlapCircle(transform.position, rangeRadius, playerLayer);
        if (detectingPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerPos = player.transform.position;

            sRender.flipX = playerPos.x < transform.position.x;

            if (!isSpawning)
            {
                isSpawning = true;
                StartCoroutine(spawnCoroutine());
            }

        }
        else
        {
            playerPos = transform.position;
        }

        Vector3 direction = (playerPos - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;

    }
    IEnumerator spawnCoroutine()
    {
        yield return new WaitForSeconds(spawnTime);
        spawnBomber();
        isSpawning = false;

    }
    void spawnBomber()
    {
        Instantiate(bomber, transform.position + Vector3.up * 0.6f, transform.rotation);
    }
}
