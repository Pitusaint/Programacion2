using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    // Aqui utilizamos un singleton, utilizando unicamente una instancia en toda la escena 
    private static ObjectPooling instance;

    //Utilizamos una cola donde vamos a  ir almacenando todos los objetos desde el principal
    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();
    //En este lugar hemos creado el GameObject para que todos los demas objetos que se vayan creando
    //sean hijos de este
    static Dictionary<int, GameObject> parents = new Dictionary<int, GameObject>();

    void Awake()
    {
       //Aqui no existe la instancia por lo tanto en la siguiente linea decimos que la utilice
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            //Si hay mas de una instancia le decimos que la destruya para quedarnos unicamente con una
            Destroy(this);
        }
    }

    //Con el preload vamos a cargar el numero de objectos que nosotros creamos que es necesario 
    public static void PreLoad(GameObject objectToPool, int amount)
    {
        //Cogemos la ID y lo almacenamos para utilizarlo
        int id = objectToPool.GetInstanceID();

        //Creamos el objeto padre y le cambiamos el nombre añadiendole pool
        GameObject parent = new GameObject();
        parent.name = objectToPool.name + "Pool";
        parents.Add(id, parent);

        //Aqui es donde se van a almacenar todos los objetos
        pool.Add(id, new Queue<GameObject>());


        for (int i = 0; i < amount; i++)
        {
            CreateObject(objectToPool);
        }
    }

    
    static void CreateObject(GameObject objectToPool)
    {
        //Para crear los objetos cogemos de nuevo la ID e instaciamos un objeto de ese tipo
        int id = objectToPool.GetInstanceID();

        GameObject go = Instantiate(objectToPool) as GameObject;
        //Le ponemos como padre el objeto que hemos creado y lo desactivamos
        go.transform.SetParent(GetParent(id).transform);
        go.SetActive(false);

        //Aqui se van almacenando en la cola que hemos creado
        pool[id].Enqueue(go);                



    }

    static GameObject GetParent(int parentID)
    {
        GameObject parent;
        parents.TryGetValue(parentID, out parent);

        return parent;

        
    }

    //Para llamar a estos objetos necesitamos saber cual es la ID del objeto y si no hay objetos
    // que podamos utilizar la pool podria hacer mas objetos.
    //Si tenemos objetos disponibles no crea ninguno y activa el siguiente que se encuentra en la pool
    public static GameObject GetObject(GameObject objectToPool)
    {
        int id = objectToPool.GetInstanceID();
        
        if(pool[id].Count == 0)
        {
            CreateObject(objectToPool);
        }

        GameObject go = pool[id].Dequeue();
        go.SetActive(true);

        return go;
    }

   //Identificamos donde en que pool se encuentra el objeto y lo volvemos a meter en la cola donde
   // se encontraba y lo desactivamos
    public static void RecicleObject(GameObject objectToPool, GameObject objectToRecicle)
    {
        int id = objectToPool.GetInstanceID();

        pool[id].Enqueue(objectToRecicle);
        objectToRecicle.SetActive(false);

    }
}

