using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    public int playerNumber = 1;

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
        rb.velocity = movement * movementSpeed;
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
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Normaliza el vector si es necesario
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
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
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Normaliza el vector si es necesario
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
    }
}
