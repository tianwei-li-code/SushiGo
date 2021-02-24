using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffGenerator : MonoBehaviour{
    public int minTimeInterval;
    public int maxTimeInterval;
    public ObjectPooler[] buffs;

    private int timeInterval;
    private int buffSelector;
    private ItemGenerator itemGenerator;
    private FriendsGenerator friendsGenerator;


    // Start is called before the first frame update
    void Start(){
        itemGenerator = FindObjectOfType<ItemGenerator>();
        friendsGenerator = FindObjectOfType<FriendsGenerator>();
        Reset();
        StartCoroutine(Counter());
    }

    // Update is called once per frame
    void Update(){
        if(timeInterval == 0){
            Reset();
            
            buffSelector = Random.Range(0, buffs.Length);
            GameObject newBuff = buffs[buffSelector].GetPooledObject();

            if(newBuff.tag == "Mana"){
                itemGenerator.addBuff(newBuff);
            } else {
                friendsGenerator.generateFriend(newBuff);
            }
        }
    }

    public void Reset(){
        timeInterval = Random.Range(minTimeInterval, maxTimeInterval);
    }

    private IEnumerator Counter(){
        while(timeInterval >= 0){
            yield return new WaitForSeconds(1);
            timeInterval--;
        }
    }
}
