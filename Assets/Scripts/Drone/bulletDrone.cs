using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDrone : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireTime;


    [SerializeField] float rangeRadius;
    [SerializeField] float fireRadius;
    bool detectingPlayer;
    [SerializeField] float randomRotationMagnitude;

    bool inRange;
    bool isFiring;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 playerPos;

    SpriteRenderer sRender;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inRange = Physics2D.OverlapCircle(transform.position, fireRadius, playerLayer);
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

        if (inRange)
        {
            rb.velocity = Vector3.zero;
            if (!isFiring)
            {
                isFiring = true;
                StartCoroutine(FireCoroutine());
            }
        }
        else
        {
            Vector3 direction = (playerPos - transform.position).normalized;
            rb.velocity = new Vector2(direction.x, direction.y) * speed;
        }


    }
    void Fire()
    {
        int randomAngle = (int)Random.Range(0, randomRotationMagnitude);
        if (randomAngle % 2 == 0)
        {
            randomAngle *= -1;
        }
        Vector3 aimDirection = (playerPos - transform.position).normalized;
        aimDirection = Quaternion.Euler(0, 0, randomAngle) * aimDirection;
        bulletScript bScript = Instantiate(bullet, transform.position, transform.rotation).GetComponent<bulletScript>();
        bScript.SetDirection(aimDirection);
    }
    IEnumerator FireCoroutine()
    {
        while (inRange)
        {
            Fire();
            yield return new WaitForSeconds(fireTime);
            yield return null;
        }
        isFiring = false;
    }
}
