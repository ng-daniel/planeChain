using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeMovement2 : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    float speed;
    [SerializeField] float accelerationConst;
    [SerializeField] Vector2 dir;
    [SerializeField] float side;
    [SerializeField] float pitchDegrees;
    [SerializeField] float currentAngle;

    bool isBreaking;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = Vector2.right;

    }

    // Update is called once per frame
    void Update()
    {

        //gets new speed
        float input; //input value
        if (Input.GetKey(KeyCode.Space))
        {
            input = -2;
            print("braking");
            isBreaking = true;
        }
        else
        {
            input = 1;
            isBreaking = false;
        }
        float acceleration = input * accelerationConst;
        speed += acceleration * Time.deltaTime;

        //gets direction
        float pitchInput = -1 * Input.GetAxisRaw("Vertical");
        float pitchDeg = pitchInput * pitchDegrees;
        if (isBreaking)
        {
            pitchDeg *= 2;
        }
        if (side < 0)
        { //flip the rotational direction across the y axis if facing left side when right side up
            pitchDeg = 180 - pitchDeg;
        }
        currentAngle = GetAngle(Vector2.right, rb.velocity.normalized);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
        Vector2 newDir = Quaternion.AngleAxis(pitchDeg * Mathf.Deg2Rad, Vector3.forward) * dir;
        dir = newDir.normalized;

        if (rb.velocity.magnitude > maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (rb.velocity.magnitude < minSpeed)
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

}
