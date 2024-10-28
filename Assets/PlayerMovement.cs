using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float currentMovementSpeed = 3f;
    public float topMovementSpeed = 12f;
    public float initialMovementSpeed = 3f;
    public float aceleration = 6f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    public int playerNumber = 1;
    private Vector2 lastMovement;

    void Start()
    {
        
    }

    void Update()
    {
        // Input
        if (playerNumber == 1)
        {
            Player1Movement();
        }
        else if (playerNumber == 2)
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

        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1; // Mover hacia arriba
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1; // Mover hacia abajo
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1; // Mover hacia la izquierda
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1; // Mover hacia la derecha
        }

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

        // Normaliza el vector si es necesario
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        if (movement.magnitude > 0)
        {
            lastMovement = movement.normalized;
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

    }
    private void Player2Movement()
    {
        movement = Vector2.zero; // Reiniciar el vector de movimiento

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.y = 1; // Mover hacia arriba
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.y = -1; // Mover hacia abajo
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = -1; // Mover hacia la izquierda
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = 1; // Mover hacia la derecha
        }

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

        // Normaliza el vector si es necesario
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        if (movement.magnitude > 0)
        {
            lastMovement = movement.normalized;
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
