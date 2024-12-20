using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>(); //puntos donde patrullara la ia

    // Mostramos el grafo en pantalla
    public int Length { get => patrolPoints.Count; }
    [Header("Gizmos parameters")]
    public Color pointsColor = Color.blue;
    public float pointSize = 0.3f;
    public Color lineColor = Color.magenta;

    public struct PathPoint
    {
        public int Index;
        public Vector2 Position;
    }

    // retorna el punto mas cercano
    public PathPoint GetClosestPathPoint(Vector2 ovniPosition)
    {
        var minDistance = float.MaxValue;
        var index = -1;
        for (int i = 0; i <  patrolPoints.Count; i++)
        {
            var tempDistance = Vector2.Distance(ovniPosition, patrolPoints[i].position);
            if(tempDistance < minDistance)
            {
                minDistance = tempDistance; 
                index = i; // Captura el punto mas cercano
            }
        }
        return new PathPoint { Index = index, Position = patrolPoints[index].position };

    }

    public PathPoint GetNextPathPoint(int index)
    {
        var newIndex = index  + 1 >= patrolPoints.Count ? 0 : index +1;
        return  new PathPoint { Index = newIndex, Position = patrolPoints[newIndex].position };
    }

    private void OnDrawGizmos()
    {
        if(patrolPoints.Count == 0)
        {
            return;
        }
        for (int i = patrolPoints.Count - 1; i > 0; i--)
        {
            if(patrolPoints[i] == null)
                return;
            Gizmos.color = pointsColor; //Define el color del punto
            Gizmos.DrawSphere(patrolPoints[i].position, pointSize); // Dibuja el punto


            if(patrolPoints.Count == 1 || patrolPoints.Count == 0)
                return;

            // Trazamos la linea
            Gizmos.color = lineColor;
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i-1].position);

            // hacemos el circuito cerrado
            if (patrolPoints.Count > 2 && i == patrolPoints.Count -1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
            }
        }
    }
}
