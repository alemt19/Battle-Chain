using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData" , menuName= "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public Sprite bulletSprite;
    public float speed = 100;
    public int damage = 10;
    public float maxDistance = 10;
    public AudioClip audioClip;
}
