using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crashDrone : MonoBehaviour
{


    Rigidbody2D rb;
    [SerializeField] float speed;


    [SerializeField] float rangeRadius;
    bool detectingPlayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 playerPos;

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

        }
        else
        {
            playerPos = transform.position;
        }

        Vector3 direction = (playerPos - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
    }
}
