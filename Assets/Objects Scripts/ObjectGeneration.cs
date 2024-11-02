using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    public float cooldown;
    public float velocity;
    private float elapsedTime;
    private Vector3 randomPosition;
    public WeightedRandomList<GameObject> objectList;
    private GameObject objectCreated;

    private void Update()
    {
        if (elapsedTime > cooldown)
        {
            elapsedTime = 0;
            randomPosition = new Vector3(Random.Range(-11, 11), transform.position.y);
            objectCreated = Instantiate(objectList.GetRandom(), randomPosition, Quaternion.identity, transform.GetChild(2));
            objectCreated.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -velocity));

        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }
}
