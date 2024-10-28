using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : AIBehaviour
{
    public PatrolPath patrolPath; // Los puntos de referencia a los que ira la ia
    [Range(0.1f,1)]
    public float arrivedDistance = 1; // distancia de llegada
    public float waitTime = 0.5f;
    [SerializeField]
    private bool isWaiting = false;
    [SerializeField]
    Vector2 currentPatrolTarget = Vector2.zero; // Punto actual dondo tiene que ir la ia
    bool isInitialized = false;
    private int currentIndex = -1;

    private void Awake()
    {
        if(patrolPath == null)
        {
            patrolPath = GetComponentInChildren<PatrolPath>();
        }
    }

    public override void PerformAction(OvniController ovni, AIDetector detector)
    {
        if (!isWaiting)
        {
            if(patrolPath.Length < 2)
            {
                return;
            }
            if (!isInitialized)
            {
                var currentPathPoint = patrolPath.GetClosestPathPoint(ovni.transform.position); // retorna el punto actual o mas cercano
                this.currentIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.Position; // se actualiza el punto actual
                isInitialized = true;
            }
            if(Vector2.Distance(ovni.transform.position, currentPatrolTarget)< arrivedDistance)
            {
                isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }
            Vector2 directionToGo = currentPatrolTarget - (Vector2)ovni.ovniMover.transform.position;
            var dotProduct = Vector2.Dot(ovni.ovniMover.transform.up, directionToGo.normalized); // producto escalara
            if (dotProduct < 0.98f)
            {
                //  si no esta mirando hacia el punto, se gira

                var crossProduct = Vector3.Cross(ovni.ovniMover.transform.up, directionToGo.normalized); // producto cruz
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;  // si es positivo, gira a la izquierda, si es negativo gira a la derecha
                ovni.HandleMoveBody(new Vector2(rotationResult,1));
            }
            else
            {
                ovni.HandleMoveBody(Vector2.up);
            }
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
        currentPatrolTarget = nextPathPoint.Position;
        currentIndex = nextPathPoint.Index;
        isWaiting = false;
    }
}
