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
    public IEnumerator Apply(GameObject target, GameObject powerUpObject)
    {
        GameObject itemPool = GameObject.FindGameObjectWithTag("ItemPool");
        Turret turretScript = target.GetComponent<Turret>();
        TurretData baseTurretData = turretScript.turretData;


        turretScript.turretData = this;
        turretScript.powerUp = true;
        powerUpObject.transform.SetParent(itemPool.transform, false);
        powerUpObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(duration);
        turretScript.powerUp = false;
        turretScript.turretData = baseTurretData;
        Destroy(powerUpObject);
    }
}
