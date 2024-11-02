using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMapLimit : MonoBehaviour
{
    public float maxDistance;
    private Vector2 startPosition; // posicion inicial de la bala 
    private float conquaredDistance = 0; // distancia recorrida
    void Start()
    {
        startPosition = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        conquaredDistance = Vector2.Distance(startPosition, transform.position);
        if (conquaredDistance > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
