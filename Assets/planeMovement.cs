using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class planeMovement : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed; //maximum velocity magnitude of plane
    [SerializeField] float minSpeed; //minimum velocity magnitude before gravity kicks in
    [SerializeField] float maxPitchDeg; //maximum rate at which plane direction will rotate(deg/sec) (default positive = counter clockwise)
    float side; //side plane is facing when right side up (-1 = left, 1 = right)
    [SerializeField] Vector2 dir; //specific vector2 direction the plane's nose is pointed in

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = Vector2.right;
        Fly(maxSpeed / 2f, dir);
    }


    void Update()
    {
        //DIRECTION UPDATE
        dir = CalculateForwardsDir();

        //SPEED UPDATE
        //
        float throttleInput = 1; //input value
        if (Input.GetKey(KeyCode.Space))
        {
            throttleInput = -1;
        }
        float netAcceleration = throttleInput * acceleration;
        /*if (CheckIfGoingBackwards()) //dont let player go backwards
        {
            netAcceleration = 0;
        }
        */
        float a = netAcceleration;


        Fly(a, dir);

        //makes plane start falling if below a certain speed

        if (rb.velocity.magnitude < minSpeed)
        {
            rb.gravityScale = 2;
        }
        else
        {
            rb.gravityScale = 0;
        }

        //restricts plane to a maximum speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = dir * maxSpeed;
        }


    }
    Vector2 CalculateForwardsDir()
    {
        float pitchInput = -1 * Input.GetAxisRaw("Vertical"); //get the pitch input from player (S is positive, W is negative)
        print(pitchInput);
        float pitchDegrees = pitchInput * maxPitchDeg; //the degrees to turn per frame, will be negative if pitching forwards, positive if pitching back
        if (side < 0)
        { //flip the rotational direction across the y axis if facing left side when right side up
            pitchDegrees = 180 - pitchDegrees;
        }
        float pitchRadians = pitchDegrees * Mathf.Deg2Rad; //convert to radians



        Vector2 newForwardsDir = Quaternion.AngleAxis(pitchRadians, Vector3.forward) * dir;
        return newForwardsDir;
    }

    void Fly(float acceleration, Vector2 dir)
    {
        rb.AddForce(acceleration * dir, ForceMode2D.Force);

    }
    void Rotate(float angle)
    {

    }
    bool CheckIfGoingBackwards()
    {
        float range = 90;
        float angle = Vector2.Angle(rb.velocity.normalized, -1 * dir);
        if (Mathf.Abs(angle) < range)
        {
            return true;
        }
        return false;
    }
}
