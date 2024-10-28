using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    public List<Transform> turretBarrels; 
    public TurretData turretData;
    private bool canShoot = true; //Flag to check if the turret can shoot
    private Collider2D[] ovniColliders; // Matriz que guarda los colliders de los ovnis a los que la bala no puede dañar, como el propio ovni de donde sale la bala

    private ObjectPool bulletPool;
    [SerializeField]
    private int bulletPoolCount = 10;
    private float currentDelay = 0;
    private void Awake()
    {
        ovniColliders = GetComponentsInParent<Collider2D>(); // para ignorar la colision entre la bala y el  propio ovni
        bulletPool = GetComponent<ObjectPool>();

    }

    private void Start()
    {
        bulletPool.Initialize(turretData.bulletPrefab, bulletPoolCount);
    }

    private void Update()
    {
        if(canShoot == false)
        {
            currentDelay -= Time.deltaTime; // Resta el tiempo transcurrido desde que se disparó
            if(currentDelay <= 0)
            {
                canShoot = true; // Si el tiempo transcurrido es menor o igual a 0
            }
        }
    }



    public void Shoot()
    {
        if(canShoot)
        {
            canShoot = false; 
            currentDelay = turretData.reloadDelay; 
            foreach (var barrel in turretBarrels)
            {
                //GameObject bullet =  Instantiate(bulletPrefab);
                GameObject bullet =  bulletPool.CreateObject();
                bullet.transform.position =  barrel.position;
                bullet.transform.rotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(turretData.bulletData);
                foreach(var collider in ovniColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
                
            }
        }
    }
}
