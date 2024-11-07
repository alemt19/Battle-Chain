using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffectGen : MonoBehaviour
{
    public void CreateObject(BulletData bulletData)
    {
        GameObject collisionAnimation = Instantiate(bulletData.collisionAnimation);
        collisionAnimation.transform.position = transform.position;
    }
}
