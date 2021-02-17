using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;

    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;

    // Use this for initialization
    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update(){

        // Move forward automatically
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        // Press Space to jump only when player is on the ground
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && grounded){
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }

    }
}
