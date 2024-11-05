using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1,20)]
    [SerializeField]
    private float viewRadius = 11; // radio de vision
    [SerializeField]
    private float detectionCheckDelay = 0.1f; //  tiempo de detección
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private LayerMask playerLayerMask; // Capa donde esta el tarjet o los tarjets de la ia

    [SerializeField]
    private LayerMask visibilityLayer; // El resto de capas que puede reconocer la ia
    public int playerNumber;

    [field: SerializeField]
    public bool TargetVisible { get; private set;}
    public Transform Target { 
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        
        if (Target != null)
        {
            TargetVisible = CheckTargetVisible();
        }
        else{
            TargetVisible = false;
            Target = null;
        }
    }

    //Verificamos si el tarjet es de la capa de interes o de otras capas
    private bool CheckTargetVisible()
    {
        var result = Physics2D.Raycast(transform.position, Target.position  - transform.position, viewRadius, visibilityLayer); // Lanza un rayo  desde la posición de la IA hacia la posición del objetivo hasta el radio  de visión y si detecta algo que este en la capa visibilityLayer retorna una variable con info de ese go
        if(result.collider != null)
        {
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0; 
        }
        return false;

    }


    private void DetectTarget()
    {
        if(Target == null)
            CheckIfPlayerInRange();
        else if (Target != null)
            DetectIfOutOfRange();
    }

    private void DetectIfOutOfRange()
    {
        if (Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(transform.position, Target.position) > viewRadius)
        {
            Target = null;
        }
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision =  Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask); // Crear el  radio de detección y verifica si hay un collider  en esa zona y en esa capa, y lo retorna


        if (collision != null)
        {
            Target = collision.transform;
        }

    }

    //Rutina que llama a la funcion  de detección cada 0.1s 
    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    //Dibujar la esfera de la ia
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

}
