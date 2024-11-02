using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthBuff", menuName = "Data/HealthBuff")]
public class HealData : ScriptableObject
{
    public int healValue;

    public void Apply(GameObject target, GameObject powerUpObject)
    {
        Damagable damagableScript = target.GetComponent<Damagable>();
        damagableScript.Heal(healValue);
        Destroy(powerUpObject);
    }
}
