using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public ParticleSystem dust;
    public float moveSpeed;
    public float jumpForce;
    public int jumpNum;
    public bool grounded;
    public LayerMask whatIsGround;
    public float speedMultiplier;
    public float maxSpeedMultiplier;
    public int speedIncreaseTime;
    

    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Animator myAnimator;
    private bool isJumping;
    private int jumpNumCounter;
    private float speedIncreaseTimeCount;
    private float originSpeed;

    // Use this for initialization
    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        isJumping = false;
        jumpNumCounter = jumpNum;
        speedIncreaseTimeCount = speedIncreaseTime;
        originSpeed = moveSpeed;
        StartCoroutine(Time());
    }

    // Time countdown
    IEnumerator Time(){
        while(speedIncreaseTimeCount >= 0){
            yield return new WaitForSeconds(1);
            speedIncreaseTimeCount--;
        }
    }

    // Update is called once per frame
    void Update(){

        // Move forward automatically
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        // Press Space to jump only when player is on the ground
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && jumpNumCounter>0){
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            isJumping = true;
            jumpNumCounter--;
        }
        
        // Create dust effect and recover jump number when player land on the ground
        if(isJumping  && myRigidbody.velocity.y<0.001 && grounded){
            CreateDust();
            isJumping = false;
            jumpNumCounter = jumpNum;
        }

        // Speed up
        if(speedIncreaseTimeCount == 0){
            moveSpeed = moveSpeed * speedMultiplier > originSpeed * maxSpeedMultiplier ? 
                        moveSpeed : moveSpeed * speedMultiplier;
            speedIncreaseTimeCount = speedIncreaseTime;
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
