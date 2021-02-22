using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffGenerator : MonoBehaviour{
    public int minTimeInterval;
    public int maxTimeInterval;
    public GameObject[] buffs;

    private int timeInterval;
    private int buffSelector;
    private ItemGenerator itemGenerator;


    // Start is called before the first frame update
    void Start(){
        timeInterval = Random.Range(minTimeInterval, maxTimeInterval);
        itemGenerator = FindObjectOfType<ItemGenerator>();
        StartCoroutine(Counter());
    }

    // Update is called once per frame
    void Update(){
        if(timeInterval == 0){
            timeInterval = Random.Range(minTimeInterval, maxTimeInterval); 

            buffSelector = Random.Range(0, buffs.Length);
            GameObject newBuff = buffs[buffSelector];

            if(newBuff.tag == "Mana"){
                itemGenerator.addBuff(newBuff);
            } else {
                // oto and komori
            }
        }
    }

    private IEnumerator Counter(){
        while(timeInterval >= 0){
            yield return new WaitForSeconds(1);
            timeInterval--;
        }
    }
}
