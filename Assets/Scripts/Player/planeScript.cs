using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class planeScript : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;

    [SerializeField] GameObject brakeLight;
    [SerializeField] GameObject throttleLight;

    [Space]
    [Header("Movement")]
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    float speed;
    [SerializeField] float accelerationConst;
    [SerializeField] Vector2 dir;
    [SerializeField] float side;
    [SerializeField] float pitchDegrees;
    [SerializeField] float currentAngle;
    bool isBreaking;

    [SerializeField] float maxFlightHeight;

    public GameObject explosion;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = Vector2.right;
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        //gets new speed
        float input; //input value

        brakeLight.SetActive(false);
        throttleLight.SetActive(false);
        input = 0;
        isBreaking = false;

        if (Input.GetKey(KeyCode.Space))
        {
            input = 1;
            isBreaking = false;
            brakeLight.SetActive(false);
            throttleLight.SetActive(true);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            input = -1;
            isBreaking = true;

            brakeLight.SetActive(true);
            throttleLight.SetActive(false);

        }

        float acceleration = input * accelerationConst;
        speed += acceleration * Time.deltaTime;

        //gets direction
        float pitchInput = -1 * side * Input.GetAxis("Horizontal");
        float pitchDeg = pitchInput * pitchDegrees * Time.deltaTime * 100f;
        if (isBreaking)
        {
            pitchDeg *= 3f;
        }

        currentAngle = GetAngle(Vector2.right, rb.velocity.normalized);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
        Vector2 newDir = Quaternion.AngleAxis(pitchDeg * Mathf.Deg2Rad, Vector3.forward) * dir;
        dir = newDir.normalized;

        if (rb.velocity.magnitude > maxSpeed)
        {
            speed = maxSpeed;
        }

        if (rb.velocity.magnitude < minSpeed)
        {
            speed = minSpeed;
        }
        rb.velocity = dir * speed;

        if (transform.position.y > maxFlightHeight)
        {
            rb.velocity = new Vector2(rb.velocity.x, -1f);
        }


    }
    private static float GetAngle(Vector2 v1, Vector2 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }

    public float getSpeed()
    {
        return speed;
    }
    public float getHeight()
    {
        return transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }
    void ProcessCollision(GameObject collision)
    {
        if (collision.layer == 3)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);

        }
        if (collision.layer == 7)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }

}
