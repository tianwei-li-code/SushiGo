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
    public bool stopMoving;
    public AudioSource jumpSound;
    public AudioSource deathSound;
    public AudioSource doubleJumpSound;
    public AudioSource runSound;
    

    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    private Animator myAnimator;
    private AnimatorStateInfo animatorInfo;
    private bool grounded;
    private bool isJumping;
    private int jumpNumCounter;
    private float originSpeed;
    private float currentSpeed;
    private float originAnimSpeed;
    private float currentAnimSpeed;
    private bool tsuyoTsuyoMode;
    private bool dead;
    private float runSoundPitch;
    private float currentSoundPitch;

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
        runSoundPitch = runSound.pitch;
        currentSoundPitch = runSoundPitch;
        tsuyoTsuyoMode = false;
        dead = false;
        stopMoving = true;
    }

    // Update is called once per frame
    void Update(){

        // Move forward automatically
        myRigidbody.velocity = stopMoving ? new Vector2(0, myRigidbody.velocity.y) : new Vector2(moveSpeed, myRigidbody.velocity.y);

        if(!isJumping && !runSound.isPlaying && !stopMoving){
            runSound.Play();
        }

        // Press Space to jump only when player is on the ground
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && jumpNumCounter>0 && !stopMoving){
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            isJumping = true;
            jumpNumCounter--;

            // Play jump sound when first jump
            if(jumpNumCounter == jumpNum - 1){
                jumpSound.Play();
            }

            // Double jump animation and sound
            if(jumpNum > 1 && jumpNumCounter == jumpNum - 2){
                myAnimator.SetTrigger("doubleJump");
                doubleJumpSound.Play();
            }

            // Stop playing run sound
            runSound.Stop();
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
        myAnimator.SetBool("dead",dead);

        animatorInfo = myAnimator.GetCurrentAnimatorStateInfo(0);

        // When death animation end, restart game
        if(dead && animatorInfo.IsName("PlayerDeath") && animatorInfo.normalizedTime >= 1.0f){
            gameManager.RestartGame();
        }
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
        runSound.pitch = currentSoundPitch;
        tsuyoTsuyoMode = false;
        if(moveSpeed * speedMultiplier < originSpeed * maxSpeedMultiplier){
            IncreaseSpeed(speedMultiplier);
            currentSpeed = moveSpeed;
            currentAnimSpeed = animSpeed;
            currentSoundPitch = runSound.pitch;
        }
    }

    // Reset the animator status and speed after respawn
    public void Respawn(){
        moveSpeed = originSpeed;
        animSpeed = originAnimSpeed;
        currentSpeed = moveSpeed;
        currentAnimSpeed = animSpeed;
        runSound.pitch = runSoundPitch;
        currentSoundPitch = runSoundPitch;
        tsuyoTsuyoMode = false;
        dead = false;
        disableDoubleJump();
    }

    // Enable double jump
    public void enableDoubleJump(){
        jumpNum = 2;
        jumpNumCounter++;
    }

    // Disable double jump
    public void disableDoubleJump(){
        if(jumpNum > 1){
            jumpNum = 1;

            if( jumpNumCounter > 0){
                jumpNumCounter--;
            }
        }
    }

    public Rigidbody2D GetRigidbody2D(){
        return myRigidbody;
    }

    // Create the dust effect
    private void CreateDust(){
        dust.Play();
    }

    // Player die
    public void Die(){
        stopMoving = true;
        dead = true;
        myAnimator.SetTrigger("hurt");
        runSound.Stop();
        deathSound.Play();
    }

    // Dead condition check
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Killbox" && !dead){
            Die();
        }
    }

    // Increase player's speed
     private void IncreaseSpeed(float SpeedMultiplier){
        moveSpeed *= SpeedMultiplier;
        animSpeed *= SpeedMultiplier;
        runSound.pitch *= SpeedMultiplier;
    }
}
