using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    GameObject nave1;
    CircleCollider2D brakeArea;
    public Rigidbody2D rb;
    public float mult;
    public PlayerMovement playerMovementReference;
    float duration;
    float baseDuration = 0.05f;
    float elapsedTime;
    Vector3 direction;
    bool isNearShip = false;
    float distancePlayerToShip;
    void Start()
    {
        nave1 = GameObject.FindGameObjectWithTag("navePrueba");
        brakeArea = GameObject.FindGameObjectWithTag("BrakeArea").GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && elapsedTime >= duration)
        {
            elapsedTime = 0f;
            distancePlayerToShip = (nave1.transform.position - transform.position).magnitude;
            duration = distancePlayerToShip * baseDuration;
            rb.drag = 1.8f;
            brakeArea.radius = distancePlayerToShip / 3.3f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Trigger Collider")
        {
            isNearShip = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Trigger Collider")
        {
            isNearShip = false;
        }
    }
}
