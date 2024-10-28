using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewOvniMovementData", menuName="Data/OvniMovementData")] // para agregarlo al menu de unity
public class OvniMovementData : ScriptableObject
{
    public float maxSpeed = 10;
    public float rotationSpeed = 100;
    public float acceleration = 70;
    public float deacceleration = 50;
}
