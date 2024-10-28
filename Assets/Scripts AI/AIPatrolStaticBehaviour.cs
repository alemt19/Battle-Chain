using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolStaticBehaviour : AIBehaviour
{
   public float patrolDelay = 4; // retardo entre cada patrulla

   [SerializeField]
   private Vector2 randomDirection = Vector2.zero; // direccion aleatoria
   [SerializeField]
   private float currentPatrolDelay; // retraso antes de obtener una nueva direccion aleatoria para que el ovni gire

   private void Awake()
   {
        randomDirection = Random.insideUnitCircle; // Obtiene la direccion aleatoria de la circunferencia unitaria
   } 

   public override void PerformAction(OvniController ovni, AIDetector detector)
   {
        float angle = Vector2.Angle(ovni.aimTurret.transform.right, randomDirection); //calcula el angulo entre ambos vectores
        if(currentPatrolDelay <= 0 && (angle < 2))
        {
            randomDirection = Random.insideUnitCircle;
            currentPatrolDelay = patrolDelay; // reinicia el retraso
        }
        else
        {
            if(currentPatrolDelay > 0)
                currentPatrolDelay -= Time.deltaTime;
            else
            ovni.HandleTurretMovement((Vector2)ovni.aimTurret.transform.position + randomDirection);

        }
   }
}
