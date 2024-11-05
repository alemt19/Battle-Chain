using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    public float cooldown;
    public float velocity;
    public float velocityTorque;
    private float elapsedTime;
    private Vector3 randomPosition;
    public WeightedRandomList<GameObject> objectList;
    private GameObject objectCreated;
    private Camera myCamera;
    private float height;
    private float width;
    private List<float> optionHeight = new List<float>();
    private List<float> optionWidth = new List<float>();
    private System.Random random = new System.Random();
    private float valueHeight;
    private int indexHeigt;
    private float valueWidth;
    private Vector3 vectorVelocity; 

    private void Awake()
    {
        myCamera = Camera.main;
        height = myCamera.orthographicSize * 2;
        width = height * myCamera.aspect;
        optionWidth.Add(0.0f);
        optionWidth.Add(-width/2 - 1/2);
        optionWidth.Add(width/2 + 1/2);
        optionHeight.Add(0.0f);
        optionHeight.Add(-height/2 - 1/2);
        optionHeight.Add(height/2 + 1/2);
        velocityTorque = 10.0f;

    }
    private void Update()
    {
        if (elapsedTime > cooldown)
        {
            elapsedTime = 0;
            optionWidth[0] = Random.Range(-width/2 + 1, width/2 - 1);
            optionHeight[0] = Random.Range(-height/2 + 1, height/2 - 1);
            indexHeigt = random.Next(3);
            valueWidth = optionWidth[indexHeigt];
            if(indexHeigt == 0)
            {
                valueHeight = optionHeight[random.Next(1, 3)];
                if(valueHeight>0)
                {
                    // Bucle hasta que obtengas un punto en el tercer o cuarto cuadrante
                    do
                    {
                        vectorVelocity = Random.insideUnitCircle.normalized;
                    } while (vectorVelocity.y > 0); // Solo permite valores en el tercer y cuarto cuadrante
                }
                else
                {
                    // Bucle hasta que obtengas un punto en el primer o segundo cuadrante
                    do
                    {
                        vectorVelocity = Random.insideUnitCircle.normalized;
                    } while ( vectorVelocity.y < 0); // Solo permite valores en el primer y segundo cuadrante
                }

            }
            else
            {
                valueHeight = optionHeight[0];

                if(valueWidth < 0)
                {
                    // Bucle hasta que obtengas un punto en el primer o segundo cuadrante
                    do
                    {
                        vectorVelocity = Random.insideUnitCircle.normalized;
                    } while (vectorVelocity.x < 0); // Solo permite valores en el primer y segundo cuadrante
                }
                else
                {
                    // Bucle hasta que obtengas un punto en el tercer o segundo cuadrante
                    do
                    {
                        vectorVelocity = Random.insideUnitCircle.normalized;
                    } while (vectorVelocity.x > 0); // Solo permite valores en el tercer y segundo cuadrante
                }
            }

            randomPosition = new Vector3(valueWidth, valueHeight);
            objectCreated = Instantiate(objectList.GetRandom(), randomPosition, Quaternion.identity, transform.GetChild(2));
            objectCreated.GetComponent<Rigidbody2D>().AddForce(vectorVelocity*velocity);
            objectCreated.GetComponent<Rigidbody2D>().AddTorque(velocityTorque);

        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }
}
