using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{
    static Dictionary<int, List<GameObject>> dictionaryOfPooledObjects = new Dictionary<int, List<GameObject>>();

    public static void PoolObject(GameObject desiredObject, int amount, Transform parent){
        List<GameObject> tempPooledObjectList = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject clone = MonoBehaviour.Instantiate(desiredObject, parent);
            tempPooledObjectList.Add(clone);
            clone.SetActive(false);
        }


        dictionaryOfPooledObjects.Add(desiredObject.GetInstanceID(), tempPooledObjectList);
    }


    public static GameObject GetPooledObject(GameObject desiredObject){
        List<GameObject> desiredPooledObjects = new List<GameObject>();
        dictionaryOfPooledObjects.TryGetValue(desiredObject.GetInstanceID(), out desiredPooledObjects);
        for (int i = 0; i < desiredPooledObjects.Count; i++)
        {
            if(!desiredPooledObjects[i].activeInHierarchy) return desiredPooledObjects[i];
        }

        return null;
    }


    public static GameObject Dynamic_GetPooledObject(GameObject desiredObject){
        List<GameObject> desiredPooledObjects = new List<GameObject>();
        dictionaryOfPooledObjects.TryGetValue(desiredObject.GetInstanceID(), out desiredPooledObjects);
        for (int i = 0; i < desiredPooledObjects.Count; i++)
        {
            if(!desiredPooledObjects[i].activeInHierarchy) return desiredPooledObjects[i];
        }

        GameObject obj = MonoBehaviour.Instantiate(desiredObject, desiredPooledObjects[0].transform.parent);
        dictionaryOfPooledObjects[desiredObject.GetInstanceID()].Add(obj);
        return obj;
    }

}
