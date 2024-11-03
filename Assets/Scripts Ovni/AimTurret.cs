using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    public float turretRotationSpeed = 150;
    public void Aim(Vector2 inputPointerPosition)
    {
        var turretDirection = (Vector3)inputPointerPosition - transform.position; // Posicion del mouse en la pantalla (el vector de posicion)
        var desiredAngle =Mathf.Atan2(turretDirection.y, turretDirection.x)*Mathf.Rad2Deg; // desiredAngle es el ángulo en grados que la torreta debe alcanzar para apuntar hacia pointerPosition.

        /*  ajuste angulo?
        if (desiredAngle > 45+90)
        {
            desiredAngle = 45+90;
        }
        else if (desiredAngle < -45+90)
        {
            desiredAngle = -45+90;
        }
        */

        var rotationStep = turretRotationSpeed * Time.deltaTime; // rotationStep es la cantidad máxima en grados que la torreta rotará en este cuadro.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0, desiredAngle - 90), rotationStep);
    }

}
