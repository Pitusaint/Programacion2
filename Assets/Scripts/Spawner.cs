using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject cube;
    [SerializeField]
    GameObject sphere;

    void Start()
    {
        ObjectPooling.PreLoad(cube, 5);
        ObjectPooling.PreLoad(sphere, 5);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Vector3 vector = SpawnPosition();

            GameObject c = ObjectPooling.GetObject(cube);
            c.transform.position = vector;
            StartCoroutine(DeSpawn(cube, c, 5f));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 vector = SpawnPosition();
            GameObject s = ObjectPooling.GetObject(sphere);
            s.transform.position = vector;
            StartCoroutine(DeSpawn(sphere, s, 5f));
        }

    }

        Vector3 SpawnPosition()
        {

            float x = Random.Range(-7f, 7f);
            float y = Random.Range(-15f, -1f);
            float z = 0f;

            Vector3 vector = new Vector3(x, y, z);
            return vector;
        }

    
        IEnumerator DeSpawn(GameObject primitive, GameObject go, float time)
        {
            yield return new WaitForSeconds(time);
            ObjectPooling.RecicleObject(primitive, go);
        }
    
    
    

}
