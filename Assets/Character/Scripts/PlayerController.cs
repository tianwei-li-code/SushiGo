using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public ParticleSystem dust;
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;

    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Animator myAnimator;
    private bool isJumping;
    private bool isFalling;

    // Use this for initialization
    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        isJumping = false;
        isFalling = false;
    }

    // Update is called once per frame
    void Update(){

        // Move forward automatically
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        // Press Space to jump only when player is on the ground
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && grounded){
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            isJumping = true;
        }

        // Create dust effect when player land on ground
        if(isJumping && myRigidbody.velocity.y>0 && myRigidbody.velocity.y<0.001){
            isFalling = true;
        }
        
        if(isFalling && grounded){
            CreateDust();
            isJumping = false;
            isFalling = false;
        }

        // Set animator's value
        myAnimator.SetFloat("velocityX",myRigidbody.velocity.x);
        myAnimator.SetFloat("velocityY",myRigidbody.velocity.y);
        myAnimator.SetBool("grounded",grounded);
    }

    // Create the dust effect
    void CreateDust(){
        dust.Play();
    }
}
