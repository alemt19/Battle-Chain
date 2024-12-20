using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniMover : MonoBehaviour
{
    private Vector2 movementVector;
    public OvniMovementData movementData;
    public Rigidbody2D rb2d;
    public float currentSpeed = 0;
    public float currentForewardDirection = 1;
    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
            
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y)>0)
        {
            currentSpeed += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= movementData.deacceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed,0,movementData.maxSpeed);
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);
        if (movementVector.y > 0)
        {
            currentForewardDirection = 1;
        }
        else if(movementVector.y<0)
        {
            currentForewardDirection = -1;
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * currentSpeed * currentForewardDirection * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0,0,-movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime));
    }
}
