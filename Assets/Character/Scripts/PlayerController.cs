using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D myRigidbody;

    // Use this for initialization
    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){

        // Move to right automatically
        myRigidbody.velocity = new Vector2(moveSpeed,myRigidbody.velocity.y);

        // Press Space to jump
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouse){

        }

    }
}
