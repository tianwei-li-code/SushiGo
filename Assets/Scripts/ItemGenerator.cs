using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour{
    // Start is called before the first frame update
    public Transform generationPoint;
    public ObjectPooler[] objectPools;
    public float minDistance;
    public float maxDistance;
    public Transform maxHeightPoint;

    private int itemSelector;
    private float distance;
    private float minHeight;
    private float maxHeight;
    private float newHeight;

    void Start(){
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
    }

    // Update is called once per frame
    void Update(){
        if(transform.position.x < generationPoint.position.x){

            // Generate random distance between two items
            distance = Random.Range(minDistance, maxDistance);

            // Choose random items from objectPools
            itemSelector = Random.Range(0, objectPools.Length);

            // If item's tag is Sushi, generate random height
            newHeight = objectPools[itemSelector].tag == "Sushi" ? Random.Range(minHeight, maxHeight) : minHeight;
            transform.position = new Vector3(transform.position.x + distance, newHeight, transform.position.z);
            
            // Generate the item
            GameObject newItem = objectPools[itemSelector].GetPooledObject();
            newItem.transform.position = transform.position;
            newItem.transform.rotation = transform.rotation;
            newItem.SetActive(true);
        }
    }
}
