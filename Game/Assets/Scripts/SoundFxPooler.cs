using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxPooler : MonoBehaviour
{
    public static SoundFxPooler current;
    public GameObject pooledObject;
    public int pooledAmount=10;
    public bool willGrow = true;
    private List<GameObject> pooledObjects;

    private void Awake()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];

            if (willGrow) { 

                GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
            }
            
        }
        return null;
    }
}
