using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShootBehaviour : AIBehaviour
{

    private float[] percentages = new float[5] { 0.05f + 0.40f, 0.10f + 0.40f, 0.15f + 0.40f, 0.20f + 0.40f, 0.0f};
    private float[] signs = new float[2] { -1.0f, 1.0f };
    private float deviationX;
    private float deviationY;
    private float sign;
    private System.Random random = new System.Random();
    public float fieldOfVisionForShooting = 60; // rango de grados para la torreta y el player, sobre los cuales la ia comenzara a disparar

    public override void PerformAction(OvniController ovni, AIDetector detector)
    {
        // Si el objetivo esta en el campo de vision, se activa el comportamiento de dispar
        if(TargetInFOV(ovni, detector))
        {
            //ovni.HandleMoveBody(Vector2.zero); // paramos el ovni pq va a disparar
            ovni.HandleShoot();
        }
        
        //Dificultamos  el disparo
        sign =  signs[random.Next(0, 2)];
        deviationX = percentages[random.Next(0, percentages.Length)]*sign;
        sign =  signs[random.Next(0, 2)];
        deviationY = percentages[random.Next(0, percentages.Length)]*sign;


        ovni.HandleTurretMovement(detector.Target.position + (new Vector3(detector.Target.position.x*deviationX, detector.Target.position.y*deviationY, 0)));   //  Movemos la torreta hacia el objetivo
    }

    private bool TargetInFOV (OvniController ovni,  AIDetector detector)
    {

        var direction = detector.Target.position - ovni.aimTurret.transform.position;
        if(Vector2.Angle(ovni.aimTurret.transform.right, direction)< fieldOfVisionForShooting/2 + 90)
        {
            // La torreta esta en el rango de +- 30 grados con el target
            return true;
        }
        return false;
    }

}
