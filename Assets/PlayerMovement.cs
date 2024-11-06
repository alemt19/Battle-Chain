using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    float currentMovementSpeed = 0f;
    public float topMovementSpeed = 20f;
    public float initialMovementSpeed = 0f;
    public float aceleration = 10f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    private Vector2 lastMovement;

    void Update()
    {
        // Input
        if (gameObject.name == "Player 1")
        {
            Player1Movement();
        }
        else if (gameObject.name == "Player 2")
        {
            Player2Movement();
        }
    }

    private void FixedUpdate()
    {
        // Movement
        rb.AddForce((currentMovementSpeed) * lastMovement);
    }

    private void Player1Movement()
    {
        movement = Vector2.zero; // Reiniciar el vector de movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        ManageIdle(movement.x , movement.y);

        // Calculos para que aumente la velocidad de forma lineal
        if (movement.magnitude >= 1 && currentMovementSpeed < topMovementSpeed)
        {
            currentMovementSpeed += aceleration * Time.deltaTime;
        }
        else if (movement.magnitude == 0 && currentMovementSpeed > initialMovementSpeed)
        {
            currentMovementSpeed -= aceleration * Time.deltaTime;
        }
        else if (currentMovementSpeed < initialMovementSpeed)
        {
            currentMovementSpeed = initialMovementSpeed;
        }

        float distancia = Vector2.Distance(lastMovement, movement);
        bool condicional = movement!= new Vector2(0,0);
        if (  Mathf.Sqrt(2)<= distancia && condicional) {
            currentMovementSpeed = initialMovementSpeed;
        }
        else if (  Mathf.Abs(1-distancia) < 0.1f && condicional) {
            currentMovementSpeed -= currentMovementSpeed/2;
        }



        // Normaliza el vector si es necesario
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        if (movement.magnitude > 0)
        {
            lastMovement = movement;
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void Player2Movement()
    {
        movement = Vector2.zero; // Reiniciar el vector de movimiento

        movement.x = Input.GetAxisRaw("Horizontal1");
        movement.y = Input.GetAxisRaw("Vertical1");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        ManageIdle(movement.x, movement.y);

        // Calculos para que aumente la velocidad de forma lineal
        if (movement.magnitude >= 1 && currentMovementSpeed < topMovementSpeed)
        {
            currentMovementSpeed += aceleration * Time.deltaTime;
        }
        else if (movement.magnitude == 0 && currentMovementSpeed > initialMovementSpeed)
        {
            currentMovementSpeed -= aceleration * Time.deltaTime;
        }
        else if (currentMovementSpeed < initialMovementSpeed)
        {
            currentMovementSpeed = initialMovementSpeed;
        }

        float distancia = Vector2.Distance(lastMovement, movement);
        bool condicional = movement != new Vector2(0, 0);
        if (Mathf.Sqrt(2) <= distancia && condicional)
        {
            currentMovementSpeed = initialMovementSpeed;
        }
        else if (Mathf.Abs(1 - distancia) < 0.1f && condicional)
        {
            currentMovementSpeed -= currentMovementSpeed / 2;
        }



        // Normaliza el vector si es necesario
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        if (movement.magnitude > 0)
        {
            lastMovement = movement;
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void ManageIdle(float moveX, float moveY)
    {
        if(moveX != 0 || moveY != 0)
        {
            animator.SetFloat("LastX", moveX);
            animator.SetFloat("LastY", moveY);
        }
    }
}
