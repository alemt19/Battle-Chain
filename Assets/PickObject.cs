using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    GameObject objectPosition;
    GameObject pickedObject;
    Collider2D collider;
    float elapsedTime = 0f;

    void Start()
    {
        objectPosition = GameObject.FindGameObjectWithTag("ObjectPosition");
    }

    void Update()
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
            collider.enabled = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
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
            collider.enabled = false;
            pickedObject.transform.SetParent(objectPosition.transform, false); // establece el padre
            pickedObject.transform.position = objectPosition.transform.position;
            pickedObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
