using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour
{
    [SerializeField]
    private AIBehaviour shootBehaviour, patrolBehaviour;
    [SerializeField]
    private OvniController ovni;
    [SerializeField]
    private AIDetector detector;
    private void Awake()
    {
        detector =  GetComponentInChildren<AIDetector>();
        ovni =  GetComponentInChildren<OvniController>();
    }

    private void Update()
    {
        if(detector.TargetVisible)
        {
            shootBehaviour.PerformAction(ovni, detector);
            patrolBehaviour.PerformAction(ovni, detector);
        }
        else
        {
            patrolBehaviour.PerformAction(ovni, detector);
        }
    }

}
