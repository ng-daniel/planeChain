using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeScript : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;
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

    [Space]
    [Header("State")]
    PlaneState currentState;
    public enum PlaneState
    {
        FLYING,
        DEAD,
        ROLLING,

    }
    [Space]
    [Header("Roll Nums")]
    [SerializeField] float rollTime;



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

        switch (currentState)
        {
            case PlaneState.FLYING:

                //gets new speed
                float input; //input value
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    input = -1;
                    print("braking");
                    isBreaking = true;
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    input = 1;
                    isBreaking = false;
                }
                else
                {
                    input = 0;
                    isBreaking = false;
                }
                float acceleration = input * accelerationConst;
                speed += acceleration * Time.deltaTime;

                //gets direction
                float pitchInput = -1 * side * Input.GetAxis("Vertical");
                float pitchDeg = pitchInput * pitchDegrees;
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

                if (Input.GetKeyDown(KeyCode.D))
                {
                    currentState = PlaneState.ROLLING;
                    StartCoroutine(rollCoroutine());
                }

                break;
            case PlaneState.ROLLING:

                break;
            case PlaneState.DEAD:
                break;
        }


        if (rb.velocity.magnitude < minSpeed)
        {
            speed = minSpeed;
        }
        rb.velocity = dir * speed;


    }
    bool IsGoingBackwards()
    {
        float range = 90;
        float angle = Vector2.Angle(rb.velocity.normalized, -1 * dir);
        if (Mathf.Abs(angle) < range)
        {
            return true;
        }
        return false;
    }
    private static float GetAngle(Vector2 v1, Vector2 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }
    void FlipPlane()
    {

    }
    IEnumerator rollCoroutine()
    {
        speed = speed * 1.5f;
        side *= -1;
        sprite.flipY = !sprite.flipY;
        yield return new WaitForSeconds(rollTime);
        currentState = PlaneState.FLYING;
    }

}
