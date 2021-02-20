using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour{
    // Start is called before the first frame update
    public Transform generationPoint;
    public ObjectPooler[] objectPools;
    public float minDistance;
    public float maxDistance;

    private int itemSelector;
    private float distance;
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        if(transform.position.x < generationPoint.position.x){
            distance = Random.Range(minDistance, maxDistance);

            itemSelector = Random.Range(0, objectPools.Length);
            
            transform.position = new Vector3(transform.position.x + distance, transform.position.y, 
                                            transform.position.z);
            
            GameObject newItem = objectPools[itemSelector].GetPooledObject();
            newItem.transform.position = transform.position;
            newItem.transform.rotation = transform.rotation;
            newItem.SetActive(true);
        }
    }
}
