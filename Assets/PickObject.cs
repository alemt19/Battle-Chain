using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    GameObject objectPosition;
    GameObject pickedObject;
    new Collider2D collider;
    float elapsedTime = 0f;

    void Start()
    {
        objectPosition = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (gameObject.name == "Player 1")
        {
            Update1();
        }
        else if (gameObject.name == "Player 2")
        {
            Update2();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (gameObject.name == "Player 1")
        {
            OnCollision1(collision);
        }
        else if (gameObject.name == "Player 2")
        {
            OnCollision2(collision);
        }
    }

    void Update1()
    {
        if (elapsedTime < 0.5)
        {
            elapsedTime += Time.deltaTime;
        }
        if (
            Input.GetKey(KeyCode.Q) &&
            objectPosition.transform.childCount == 1 &&
            pickedObject != null &&
            elapsedTime >= 0.5f)
        {
            elapsedTime = 0;
            pickedObject.transform.SetParent(null);
            pickedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            pickedObject = null;
            collider.isTrigger = false;
        }
    }

    void Update2()
    {
        if (elapsedTime < 0.5)
        {
            elapsedTime += Time.deltaTime;
        }
        if (
            Input.GetKey(KeyCode.U) &&
            objectPosition.transform.childCount == 1 &&
            pickedObject != null &&
            elapsedTime >= 0.5f)
        {
            elapsedTime = 0;
            pickedObject.transform.SetParent(null);
            pickedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            pickedObject = null;
            collider.isTrigger = false;
        }
    }

    void OnCollision1(Collision2D collision)
    {
        if (
            Input.GetKey(KeyCode.Q) &&
            (collision.gameObject.layer == 9 || collision.gameObject.layer == 13) &&
            objectPosition.transform.childCount == 0 &&
            elapsedTime >= 0.5f
            )
        {
            elapsedTime = 0;
            pickedObject = collision.gameObject;
            collider = pickedObject.GetComponent<Collider2D>();
            collider.isTrigger = true;
            pickedObject.transform.SetParent(objectPosition.transform, false);
            pickedObject.transform.position = objectPosition.transform.position;
            pickedObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void OnCollision2(Collision2D collision)
    {
        if (
            Input.GetKey(KeyCode.U) &&
            (collision.gameObject.layer == 9 || collision.gameObject.layer == 13) &&
            objectPosition.transform.childCount == 0 &&
            elapsedTime >= 0.5f
            )
        {
            elapsedTime = 0;
            pickedObject = collision.gameObject;
            collider = pickedObject.GetComponent<Collider2D>();
            collider.isTrigger = true;
            pickedObject.transform.SetParent(objectPosition.transform, false);
            pickedObject.transform.position = objectPosition.transform.position;
            pickedObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
