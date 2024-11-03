using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 finalPosition;
    public float elapsedTime;
    public float duration;
    void Start()
    {
        startPosition = transform.parent.GetChild(2).localPosition;
        finalPosition = transform.parent.GetChild(3).localPosition;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.y <= finalPosition.y)
        {
            transform.localPosition = startPosition;
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.01f);
        }
    }
}
