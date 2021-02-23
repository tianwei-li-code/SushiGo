using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour{
    
    public float speedMultiplier;
    public GameObject manaRecoverPoint;

    private float moveSpeed;
    private Rigidbody2D manaRigidbody;
    private Collider2D manaCollider;
    private Animator manaAnimator;
    private PlayerController nano;
    private float animSpeed;

    // Start is called before the first frame update
    void Start(){
        manaRigidbody = GetComponent<Rigidbody2D>();
        manaCollider = GetComponent<Collider2D>();
        manaAnimator = GetComponent<Animator>();
        nano = FindObjectOfType<PlayerController>();
        reset();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        this.moveSpeed = nano.moveSpeed * speedMultiplier;
        this.animSpeed = nano.animSpeed;

        manaAnimator.SetFloat("animSpeed",animSpeed);

        manaRigidbody.velocity = new Vector2(moveSpeed, manaRigidbody.velocity.y);

        if(transform.position.x >= manaRecoverPoint.transform.position.x){
            reset();
        }
    }

    public void reset(){
        if(gameObject.activeInHierarchy){
            gameObject.SetActive(false);
        }
    }
}
