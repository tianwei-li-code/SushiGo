using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtoDragonController : MonoBehaviour{
    public float buffLastTime;
    public float speedMultiplier;
    public float radiusMultiplier;
    public bool canBeActivated;

    private float buffLastTimeCount;
    private float speed;
    private PlayerController player;
    private Transform playerTransform;
    private Rigidbody2D otoRigidbody;
    private float defaultRadius;
    private CircleCollider2D otoCollider;
    private GameObject terminalPoint;

    // Start is called before the first frame update
    void Start(){
        player = FindObjectOfType<PlayerController>();
        otoRigidbody = GetComponent<Rigidbody2D>();
        otoCollider = GetComponent<CircleCollider2D>();
        terminalPoint = GameObject.FindGameObjectWithTag("GenerationPoint");
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        defaultRadius = otoCollider.radius;
        reset();
    }

    // Update is called once per frame
    void Update(){

        // When buff activated
        if(playerTransform != null && buffLastTimeCount > 0){
            otoRigidbody.velocity = new Vector2(0, otoRigidbody.velocity.y);
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if(distance < 0.05f){
                speed = player.moveSpeed * 0.9f;
            } else if (distance > 0.35f){
                speed = player.moveSpeed * 1.05f;
            }
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position,
                                                        speed * Time.deltaTime);
        } 
        
        // If not being activated, move to the terminal point
        else if (buffLastTimeCount == 0){
            speed = player.moveSpeed * speedMultiplier;
            otoRigidbody.velocity = new Vector2(speed, otoRigidbody.velocity.y);
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
            otoCollider.enabled = true;
            otoCollider.radius = defaultRadius;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        // When player get oto dragon, start buff time counter and activate magnet ability
        if(other.gameObject.tag == "Player" && canBeActivated){
            otoCollider.radius *= radiusMultiplier;
            buffLastTimeCount = buffLastTime;
            canBeActivated = false;
            StartCoroutine(Counter());
        }
    }
    
    // Buff last time counter, when buff end, disable collider to 0 to avoid geting sushi
    private IEnumerator Counter(){
        while(buffLastTimeCount > 0){
            yield return new WaitForSeconds(1);
            buffLastTimeCount--;
            if(buffLastTimeCount == 0){
                otoCollider.enabled = false;
            }
        }
    }
}