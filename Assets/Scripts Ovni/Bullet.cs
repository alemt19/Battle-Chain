using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private Vector2 startPosition; // posicion inicial de la bala 
    private float conquaredDistance = 0; // distancia recorrida
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Damagable damagableScript;
    private bool damagableScriptActive;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        damagableScript = gameObject.GetComponent<Damagable>();
    }

    public void Initialize(BulletData bulletData)
    {
        this.bulletData = bulletData;
        startPosition = transform.position;
        rb2d.velocity = transform.up * this.bulletData.speed;
        sr.sprite = bulletData.bulletSprite;
        if (bulletData.damage >= 30)
        {
            damagableScript.enabled = true;
            damagableScriptActive = true;
        }
        else
        {
            damagableScript.enabled = false;
            damagableScriptActive = true;
        }
        
    }

    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPosition); 
        if (conquaredDistance > bulletData.maxDistance)
        {
            DisableObject();
        }
    }

    public void DisableObject()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false); // desactiva el go de la escena pero no lo elimina
    }
    public void DisableObjectAndResetHealth()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false); // desactiva el go de la escena pero no lo elimina
        damagableScript.Heal(damagableScript.MaxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colliderd " + collision.name);

        var damageble = collision.GetComponent<Damagable>();
        if (damageble != null)
        {
            damageble.Hit(bulletData.damage);
        }

        if (!damagableScriptActive)
        {
            DisableObject();
        }
        else if (damagableScriptActive && collision.gameObject.layer != 6)
        {
            DisableObjectAndResetHealth();
        }
    }


}
