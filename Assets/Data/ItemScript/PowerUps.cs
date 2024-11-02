using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    GameObject ovniTurret;
    public TurretData powerUpData;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("navePrueba"))
        {
            ovniTurret = collision.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
            if (!ovniTurret.GetComponent<Turret>().powerUp)
            {
                StartCoroutine(powerUpData.Apply(ovniTurret, gameObject));
            }
        }
    }
}
