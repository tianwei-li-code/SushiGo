using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour{

    public Transform generationPoint;
    public ObjectPooler[] objectPools;

    private float platformWidth;
    private int platformSelector;

    // Start is called before the first frame update
    void Start(){
        platformWidth = objectPools[0].pooledObject.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update(){
        if(transform.position.x < generationPoint.position.x){
            transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y, transform.position.z);
            platformSelector = Random.Range(0, objectPools.Length);

            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
        }
    }
}
