using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniMover : MonoBehaviour
{
    private Vector2 movementVector;
    public Rigidbody2D rb2d;
    public float maxSpeed = 10;
    public float rotationSpeed = 100;

    public float accelaration  = 70;
    public  float deacceleration = 500;
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
            currentSpeed += accelaration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed,0,maxSpeed);
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
        Debug.Log(rb2d.velocity);
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0,0,-movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }
}
