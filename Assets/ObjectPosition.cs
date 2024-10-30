using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPosition : MonoBehaviour
{
    float lastX, lastY;
    Animator playerAnimator;
    float basePosition = 0.27f;
    float baseDiagonalPosition = 0.20f;
    Vector3 newLocalPosition;

    private void Start()
    {
        playerAnimator = transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        lastX = playerAnimator.GetFloat("LastX");
        lastY = playerAnimator.GetFloat("LastY");
        if (Math.Pow(lastX, 2) == 1 && Math.Pow(lastY, 2) == 1)
        {
            transform.localPosition = new Vector3(lastX * baseDiagonalPosition, lastY * baseDiagonalPosition);
        }
        else
        {
            transform.localPosition = new Vector3(lastX * basePosition, lastY * basePosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, 0.25f);
    }
}
