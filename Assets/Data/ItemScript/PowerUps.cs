using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    GameObject target;
    public TurretData powerUpData;
    public HealData healData;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.CompareTag("Nave1") || collision.gameObject.CompareTag("Nave2")) && powerUpData != null)
        {
            target = collision.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            if (!target.GetComponent<Turret>().powerUp)
            {
                StartCoroutine(powerUpData.Apply(target, gameObject));
            }
        }
        else if ((collision.gameObject.CompareTag("Nave1") || collision.gameObject.CompareTag("Nave2")) && healData != null)
        {
            target = collision.gameObject;
            healData.Apply(target, gameObject);
        }
    }
}
