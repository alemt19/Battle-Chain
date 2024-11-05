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
    private bool targetIa;
    private float  detectionTimer1;
    private float  detectionTimer2;


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
        targetIa = false;
        detectionTimer1 = 0.5f;
        detectionTimer2 = 0.5f;
    }
    

    private void Update()
    {

        // Input
        if (playerNumber == 1)
        {
            Player1Target();
        }
        else if (playerNumber == 2)
        {
            Player2Target();
        }

        
        if (Target != null)
        {
            TargetVisible = CheckTargetVisible();
        }
        else{
            TargetVisible = false;
            Target = null;
        }
    }

    private void Player1Target()
    {
        if(Input.GetKey(KeyCode.R) && detectionTimer1 >= 0.5)
        {
            detectionTimer1 = 0;
            targetIa = !targetIa;
            if(targetIa)
            {
                playerLayerMask = LayerMask.GetMask("Player2");
                visibilityLayer |= (1 << LayerMask.NameToLayer("Player2"));
                visibilityLayer &= ~(1 << LayerMask.NameToLayer("Enemy2"));
                Target = null;
            }
            else
            {
                playerLayerMask = LayerMask.GetMask("Enemy2");
                visibilityLayer |= (1 << LayerMask.NameToLayer("Enemy2"));
                visibilityLayer &= ~(1 << LayerMask.NameToLayer("Player2"));
                Target = null;
            }
        }
        else{
            detectionTimer1 += Time.deltaTime;
        }
    }
    
    private void Player2Target()
    {
        if(Input.GetKey(KeyCode.Y) && detectionTimer2 >= 0.5)
        {
            targetIa = !targetIa;
            if(targetIa)
            {
                playerLayerMask = LayerMask.GetMask("Player1");
                visibilityLayer |= (1 << LayerMask.NameToLayer("Player1"));
                visibilityLayer &= ~(1 << LayerMask.NameToLayer("Enemy1"));
                Target = null;
            }
            else
            {
                playerLayerMask = LayerMask.GetMask("Enemy1");
                visibilityLayer |= (1 << LayerMask.NameToLayer("Enemy1"));
                visibilityLayer &= ~(1 << LayerMask.NameToLayer("Player1"));
                Target = null;
            }
        }
        else
        {
            detectionTimer2 += Time.deltaTime;
        } 
    }


    //Verificamos si el tarjet es de la capa de interes o de otras capas
    private bool CheckTargetVisible()
    {
        var result = Physics2D.Raycast(transform.position, Target.position  - transform.position, viewRadius, visibilityLayer); // Lanza un rayo  desde la posición de la IA hacia la posición del objetivo hasta el radio  de visión y si detecta algo que este en la capa visibilityLayer retorna una variable con info de ese go
        Debug.Log(result.collider.gameObject.name);
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
