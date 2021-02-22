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
    public float marginOfBuff;

    private int itemSelector;
    private float distance;
    private float minHeight;
    private float maxHeight;
    private float newHeight;
    private GameObject buff;

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

    // If buff is in the queue, generate it at random distance after new item 
    public void addBuff(GameObject buff){
        this.buff = buff;
        buff.transform.position = new Vector3(transform.position.x + Random.Range(marginOfBuff, 
                                                maxDistance-marginOfBuff), minHeight, transform.position.z);
        buff.SetActive(true);
    }
}
