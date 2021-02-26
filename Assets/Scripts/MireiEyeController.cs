using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MireiEyeController : MonoBehaviour{
    public float speedMultiplier;

    private PlayerController player;
    private GameObject playerObj;
    private Rigidbody2D mireiRigidbody;
    private GameObject eyePosition;
    private GameObject eyeRecoverPoint;
    private Animator mireiAnimator;
    private float speed;
    private bool stopMove;
    private bool beamEnd;
    private bool beamLock;
    private AnimatorStateInfo animatorInfo;
    private AudioSource beamSound;
    
    
    

    // Start is called before the first frame update
    void Start(){
        player = FindObjectOfType<PlayerController>();
        mireiRigidbody = GetComponent<Rigidbody2D>();
        mireiAnimator = GetComponent<Animator>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        eyePosition = GameObject.FindGameObjectWithTag("EyePosition");
        eyeRecoverPoint = GameObject.FindGameObjectWithTag("ItemRecoverPoint");
        gameObject.SetActive(false);
        beamSound = GameObject.Find("BeamSound").GetComponent<AudioSource>();
    }

    void Update() {
        if(transform.position.x > eyeRecoverPoint.transform.position.x){
            gameObject.SetActive(false);
        }

        animatorInfo = mireiAnimator.GetCurrentAnimatorStateInfo(0);

        // If beam emission is finished
        if(animatorInfo.IsName("BeamEmission") && animatorInfo.normalizedTime >= 1.0f){
            mireiAnimator.SetTrigger("Reset");
            beamEnd = true;
            StartCoroutine(LeaveCamera());
        }

        // If beam is locked
        if(animatorInfo.IsName("BeamReady") && animatorInfo.normalizedTime >= 1.0f){
            mireiAnimator.SetTrigger("BeamEmission");
            beamLock = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate(){

        // Enter camera, follow player's height
        if(!stopMove && !beamEnd){
            transform.position = new Vector3 (transform.position.x, playerObj.transform.position.y, transform.position.z);
        }

        // Waiting to leave
        if(beamEnd && stopMove){
            transform.position = new Vector3 (eyePosition.transform.position.x, transform.position.y, transform.position.z);
        }
        
        // Beam Ready status
        if(transform.position.x < eyePosition.transform.position.x && !beamEnd){
            // Stop moving
            mireiRigidbody.velocity = new Vector2(0, mireiRigidbody.velocity.y);

            // Change height
            float height = beamLock ? transform.position.y : playerObj.transform.position.y;
            transform.position = new Vector3 (eyePosition.transform.position.x, height, transform.position.z);

            if(!stopMove){
                mireiAnimator.SetTrigger("BeamReady");
                stopMove = true;
            }
        }
    }

    public void Reset(){
        stopMove = false;
        beamEnd = false;
        beamLock = false;
        gameObject.SetActive(true);
        EnterCamera();
    }

    public void EmissionSound(){
        beamSound.Play();
    }

    private void EnterCamera(){
        speed = player.moveSpeed * (2 - speedMultiplier);
        mireiRigidbody.velocity = new Vector2(speed, mireiRigidbody.velocity.y);
    }

    private IEnumerator LeaveCamera(){
        yield return new WaitForSeconds(0.5f);
        stopMove = false;
        speed = player.moveSpeed * speedMultiplier;
        mireiRigidbody.velocity = new Vector2(speed, mireiRigidbody.velocity.y);
    }

    // Dead zone check
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            player.Die();
        }
    }

}
