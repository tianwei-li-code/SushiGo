using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public GameManager gameManager;
    public ParticleSystem dust;
    public float moveSpeed;
    public float jumpForce;
    public int jumpNum;
    public LayerMask whatIsGround;
    public float speedMultiplier;
    public float boostSpeedMultiplier;
    public float maxSpeedMultiplier;
    public float animSpeed;
    

    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Animator myAnimator;
    private bool grounded;
    private bool isJumping;
    private int jumpNumCounter;
    private float originSpeed;
    private float currentSpeed;
    private float originAnimSpeed;
    private float currentAnimSpeed;
    private bool tsuyoTsuyoMode;

    // Use this for initialization
    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        isJumping = false;
        jumpNumCounter = jumpNum;
        animSpeed = 1;
        originSpeed = moveSpeed;
        currentSpeed = moveSpeed;
        originAnimSpeed = animSpeed;
        currentAnimSpeed = animSpeed;
        tsuyoTsuyoMode = false;
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

        // Set animator's value
        myAnimator.SetFloat("velocityX",myRigidbody.velocity.x);
        myAnimator.SetFloat("velocityY",myRigidbody.velocity.y);
        myAnimator.SetBool("grounded",grounded);
        myAnimator.SetFloat("animSpeed",animSpeed);
        myAnimator.SetBool("tsuyoTsuyoMode",tsuyoTsuyoMode);
    }

    // Enter tsuyo-tsuyo mode, increase speed and change the sprites
    public void startTsuyoTsuyoMode(){
        IncreaseSpeed(boostSpeedMultiplier);
        tsuyoTsuyoMode = true;
    }

    // Increase the move speed and the animation speed
    public void SpeedUp(){
        moveSpeed = currentSpeed;
        animSpeed = currentAnimSpeed;
        tsuyoTsuyoMode = false;
        if(moveSpeed * speedMultiplier < originSpeed * maxSpeedMultiplier){
            IncreaseSpeed(speedMultiplier);
            currentSpeed = moveSpeed;
            currentAnimSpeed = animSpeed;
        }
    }

    // Reset the animator status and speed after respawn
    public void Respawn(){
        myAnimator.SetBool("dead",false);
        moveSpeed = originSpeed;
        animSpeed = originAnimSpeed;
        currentSpeed = moveSpeed;
        currentAnimSpeed = animSpeed;
        tsuyoTsuyoMode = false;
    }

    // Create the dust effect
    private void CreateDust(){
        dust.Play();
    }

    // Dead condition check
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Killbox"){
            moveSpeed = 0;
            myAnimator.SetTrigger("hurt");
            myAnimator.SetBool("dead",true);
            gameManager.RestartGame();
        }
    }

    // Increase player's speed
     private void IncreaseSpeed(float SpeedMultiplier){
        moveSpeed = moveSpeed * SpeedMultiplier;
        animSpeed = animSpeed * SpeedMultiplier;
    }
}
