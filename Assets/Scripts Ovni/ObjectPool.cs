using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    protected GameObject objectToPool; // Objeto a ser extraido o reutilizado 
    [SerializeField]
    protected int poolSize = 10; // numeros de objetos que se crean nuevos antes de ser reutilizados
    protected Queue<GameObject> objectPool; // creara una cola, para ir guardando los objetos que se vayan liberando en orden
    protected bool isInitialized = false;
    public Transform spawnedObjectsParent; //  donde se guardaran los objetos que se van a reutilizar, para que no abarrote toda la jerarquia


    private void Awake()
    {
        objectPool = new Queue<GameObject>();
    }
    public void Initialize(GameObject objectToPool, int poolSize = 10)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded(); // aqui creamos el go padre que agrupe  a los objetos que se van a reutilizar

        GameObject spawnedObject = null;

        if (objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity); // crea una copia de un objeto (objectToPool) en la escena y lo posiciona en el mismo lugar y rotación del objeto que ejecuta este script.
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count; // crear el nombre con el name del objeto raiz de la cadena + el nombre del objectpool + su numero
            spawnedObject.transform.SetParent(spawnedObjectsParent); // En la jerarquia lo metemos en el objeto padre que creamos en el metodo CreateObjectParentIfNeeded()
            spawnedObject.AddComponent<DestroyIfDisabled>();

        }
        else 
        {
            // Comenzamos a reutilizar los objetos
            spawnedObject = objectPool.Dequeue(); // devuelve el primer elemento introducido en la cola y lo elimina de esta
            spawnedObject.transform.position =  transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }
        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    private void CreateObjectParentIfNeeded()
    {
        if (spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" +  objectToPool.name;
            var parentObject = GameObject.Find(name);
            if(parentObject != null)
            {
                spawnedObjectsParent = parentObject.transform;
            }
            else
            {
                spawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var item in objectPool)
        {
            if(item == null)
            {
                continue;
            }
            else if (item.activeSelf == false)
            {
                Destroy(item);
            }
            else
            {
                item.GetComponent<DestroyIfDisabled>().SelfDestructionEnabled = true;
            }
        }
    }
}
