using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeart : MonoBehaviour{

    private FriendsGenerator buffGenerator;
    private Animator heartAnimator;

    // Start is called before the first frame update
    void Start(){
        heartAnimator = GetComponent<Animator>();
        buffGenerator = FindObjectOfType<FriendsGenerator>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player"){
            heartAnimator.SetTrigger("Get");
            buffGenerator.generateMana();
        }
    }
}
