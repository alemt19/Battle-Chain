using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    GameObject nave1;
    public Rigidbody2D rb;
    public float mult;
    public PlayerMovement playerMovementReference;
    float duration;
    float baseDuration = 0.03f;
    float elapsedTime;
    Vector3 direction;
    bool isNearShip = false;
    float distancePlayerToShip;
    public 
    void Start()
    {
        nave1 = GameObject.FindGameObjectWithTag("navePrueba");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && elapsedTime >= duration)
        {
            elapsedTime = 0f;
            distancePlayerToShip = (nave1.transform.position - transform.position).magnitude;
            duration = distancePlayerToShip * baseDuration;
            rb.drag = 1.8f;
        }
    }

    private void FixedUpdate()
    {
        if (elapsedTime < duration && !isNearShip)
        {
            elapsedTime += Time.deltaTime;
            direction = nave1.transform.position - transform.position;
            rb.AddForce(direction * mult);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isNearShip = true;
        rb.drag = 20;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isNearShip = false;
        rb.drag = 1.8f;
    }
}
