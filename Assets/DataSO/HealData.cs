using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthBuff", menuName = "Data/HealthBuff")]
public class HealData : ScriptableObject
{
    public int healValue;
    public AudioClip audioClip;

    public void Apply(GameObject target, GameObject powerUpObject)
    {
        Damagable damagableScript = target.GetComponent<Damagable>();
        damagableScript.Heal(healValue);
        Audio.Instance.PlaySound(audioClip);
        Destroy(powerUpObject);
    }
}
