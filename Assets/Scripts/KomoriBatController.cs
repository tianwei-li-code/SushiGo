using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomoriBatController : MonoBehaviour{
    public float buffLastTime;
    public GameObject terminalPoint;
    public float speedMultiplier;

    private float buffLastTimeCount;
    private float speed;
    private PlayerController player;
    private Transform playerTransform;
    private bool canBeActivated;
    private Rigidbody2D komoriRigidbody;

    // Start is called before the first frame update
    void Start(){
        player = FindObjectOfType<PlayerController>();
        komoriRigidbody = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        reset();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        if(playerTransform != null && buffLastTimeCount > 0){
            komoriRigidbody.velocity = new Vector2(0, komoriRigidbody.velocity.y);
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            speed = distance < 0.1f ? player.moveSpeed * 0.9f : player.moveSpeed * 1.1f;
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position,
                                                        speed * Time.deltaTime);
        } 
        
        // If not being activated, disable player's double jump ability and move to the terminal point
        else if (buffLastTimeCount == 0){
            player.disableDoubleJump();
            speed = player.moveSpeed * speedMultiplier;
            komoriRigidbody.velocity = new Vector2(speed, komoriRigidbody.velocity.y);
        }

        // When arrived terminal point, deactivate the game object and reset canBeActivated status
        if (transform.position.x > terminalPoint.transform.position.x){
            reset();
            gameObject.SetActive(false);
        }
    }

    public void reset(){
        if(gameObject.activeInHierarchy){
            canBeActivated = true;
            buffLastTimeCount = 0;
        }
    }

    // When player get komori bat, start buff time counter and activate double jump ability for player
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && canBeActivated){
            player.enableDoubleJump();
            buffLastTimeCount = buffLastTime;
            canBeActivated = false;
            StartCoroutine(Counter());
        }
    }

    private IEnumerator Counter(){
        while(buffLastTimeCount > 0){
            yield return new WaitForSeconds(1);
            buffLastTimeCount--;
        }
    }
}
