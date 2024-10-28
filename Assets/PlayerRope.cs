using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    GameObject nave1;
    public Rigidbody2D rb;
    public float mult = 10f;
    public PlayerMovement playerMovementReference;
    float duration;
    float baseDuration = 0.05f;
    float elapsedTime;
    Vector3 direction;
    void Start()
    {
        nave1 = GameObject.FindGameObjectWithTag("navePrueba");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && elapsedTime >= duration)
        {
            elapsedTime = 0f;
            duration = (nave1.transform.position - transform.position).magnitude * baseDuration;
        }
    }

    private void FixedUpdate()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            direction = nave1.transform.position - transform.position;
            rb.AddForce(direction.normalized * mult);
        }
    }
}
