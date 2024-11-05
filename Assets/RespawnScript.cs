using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    GameObject player;
    GameObject rope;
    public float respawnTime;
    bool isRespawning;
    float elapsedTime;
    public float invincibleDuration;
    Damagable damagable;
    void Start()
    {
        player = transform.GetChild(1).gameObject;
        rope = transform.GetChild(2).gameObject;
        damagable = player.GetComponent<Damagable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.activeInHierarchy && elapsedTime < respawnTime)
        {
            elapsedTime += Time.deltaTime;
        }
        else if (!player.activeInHierarchy && elapsedTime > respawnTime)
        {
            rope.SetActive(true);
            player.SetActive(true);
            StartCoroutine(Invincible());
            elapsedTime = 0;
        }
    }

    IEnumerator Invincible()
    {
        damagable.Health = 9999999;
        yield return new WaitForSeconds(invincibleDuration);
        damagable.Health = damagable.MaxHealth;
    }
}
