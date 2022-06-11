using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour, IObjectPooler<GameObject>
{
    GameObject [] objects;
    int numToPool;
    GameObject objectToSpawn;

    // The first constructor takes the number of objects and the array of already instantiated objects
    public ObjectPooler(int numOfObjects, GameObject [] instantiatedObjects)
    {
        numToPool = numOfObjects;
        objects = instantiatedObjects;
    }

    // The 2nd constructor takes number of objects needed to be spawned and the object that needs to be instantiated several times
    public ObjectPooler(int numOfObjects, GameObject objectToInstantiate)
    {
        numToPool = numOfObjects;
        objectToSpawn = objectToInstantiate;

        for (int i = 0; i < numToPool; i++)
        {
            GameObject obj = Instantiate(objectToSpawn);
            objects[i] = obj;
            obj.SetActive(false);
        }
    }

    // Returns the next available, deactivated object
    public GameObject RetrievePooledObject()
    {
        for (int i = 0; i < numToPool - 1; i++)
        {
            if (!objects[i].activeInHierarchy)
                return objects[i];
        }

        // If no object was found, return null
        return null;
    }
}
