using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsGenerator : MonoBehaviour{
    public Transform generationPoint;
    public Transform maxHeightPoint;

    public GameObject mana;
    public GameObject oto;
    public GameObject komori;

    private float minHeight;
    private float maxHeight;
    private float newHeight;

    // Start is called before the first frame update
    void Start(){
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void generateMana(){
        mana.transform.position = generationPoint.position;
        mana.transform.rotation = generationPoint.rotation;
        mana.SetActive(true);
    }
}
