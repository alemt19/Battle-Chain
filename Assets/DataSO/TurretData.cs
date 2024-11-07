using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewTurretData" , menuName= "Data/TurretData")]
public class TurretData : ScriptableObject
{
    public GameObject bulletPrefab;
    public float reloadDelay = 1;
    public BulletData bulletData;
    public float duration = 5;
    public AudioClip audioClip;
    // Define el desplazamiento en el espacio local de secondTurret
    private Vector3 desplazamientoLocal = new Vector3(-0.125f, 0, 0);
    public IEnumerator Apply(GameObject target, GameObject powerUpObject)
    {
        GameObject itemPool = GameObject.FindGameObjectWithTag("ItemPool");
        Turret turretScript = target.GetComponent<Turret>();
        TurretData baseTurretData = turretScript.turretData;


        turretScript.turretData = this;
        turretScript.powerUp = true;
        powerUpObject.transform.SetParent(itemPool.transform, false);
        powerUpObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        GameObject secondTurret = null; 
        Vector3 desplazamientoGlobal = Vector3.zero; 
        OvniController ovniController = null;
        if(this.name == "DoubleTurret")
        {
            secondTurret = target.transform.parent.gameObject.transform.GetChild(1).gameObject;
            // Activa secondTurret
            secondTurret.SetActive(true);

            // Convierte el desplazamiento local al espacio global
            desplazamientoGlobal = secondTurret.transform.TransformDirection(desplazamientoLocal);

            // Aplica el desplazamiento global a la posición de target
            target.transform.position += desplazamientoGlobal;


            ovniController = target.transform.parent.parent.gameObject.GetComponent<OvniController>();
            ovniController.newAwake();
        }

        Audio.Instance.PlaySound(audioClip);
        yield return new WaitForSeconds(duration);
        turretScript.powerUp = false;
        turretScript.turretData = baseTurretData;
        if (this.name == "DoubleTurret")
        {
            secondTurret.SetActive(false);
            ovniController.newAwake();
            target.transform.position -= desplazamientoGlobal;

        }

        Destroy(powerUpObject);
    }
}
