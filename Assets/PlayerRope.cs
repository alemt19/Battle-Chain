using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    GameObject nave;
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
        nave = transform.parent.GetChild(0).GetChild(1).gameObject;
        brakeArea = transform.parent.GetChild(0).GetChild(1).GetChild(3).gameObject.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (gameObject.name == "Player 1")
        {
            PlayerRope1();
        }
        else if (gameObject.name == "Player 2")
        {
            PlayerRope2();
        }
    }

    private void FixedUpdate()
    {
        if (elapsedTime < duration && !isNearShip)
        {
            elapsedTime += Time.deltaTime;
            direction = nave.transform.position - transform.position;
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

    void PlayerRope1()
    {
        if (Input.GetKeyDown(KeyCode.E) && elapsedTime >= duration)
        {
            Debug.Log(nave.transform.position);
            elapsedTime = 0f;
            distancePlayerToShip = (nave.transform.position - transform.position).magnitude;
            duration = distancePlayerToShip * baseDuration;
            rb.drag = 1.8f;
            brakeArea.radius = distancePlayerToShip / 3.3f;
        }
    }

    void PlayerRope2()
    {
        if (Input.GetKeyDown(KeyCode.O) && elapsedTime >= duration)
        {
            elapsedTime = 0f;
            distancePlayerToShip = (nave.transform.position - transform.position).magnitude;
            duration = distancePlayerToShip * baseDuration;
            rb.drag = 1.8f;
            brakeArea.radius = distancePlayerToShip / 3.3f;
        }
    }
}
