using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeart : MonoBehaviour{

    public FriendsGenerator buffGenerator;
    private Animator heartAnimator;

    // Start is called before the first frame update
    void Start(){
        heartAnimator = GetComponent<Animator>();
        buffGenerator = FindObjectOfType<FriendsGenerator>();
        gameObject.SetActive(false);
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
