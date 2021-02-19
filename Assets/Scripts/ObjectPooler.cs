using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour{
    // Start is called before the first frame update

    public GameObject pooledObject;
    public int pooledAmount;
    List<GameObject> pooledObjects;

    void Start(){
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++){
            AddObject();
        }
    }

    public GameObject GetPooledObject(){
        for(int i = 0; i < pooledObjects.Count; i++){
            if(!pooledObjects[i].activeInHierarchy){
               return pooledObjects[i]; 
            }
        }
        AddObject();
        return pooledObjects[pooledObjects.Count-1];
    }

    private void AddObject(){
        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);
    }
}
