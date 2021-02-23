using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour{
    public int scoreToGive;
    public bool getSushi = false;

    private Vector3 sushiUIPosition;
    private ScoreManager scoreManager;
    private Vector3 originPosition;

    // Start is called before the first frame update
    void Start(){
        scoreManager = FindObjectOfType<ScoreManager>();
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update(){

        // If player get sushi, move sushi to the UI position
        if(getSushi){
            MoveSushi();
        }
    }

    // Check if player get the sushi
    // Check if oto is activated and get sushi
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" || (other.gameObject.tag == "Oto" && 
                                                !other.gameObject.GetComponent<OtoDragonController>().canBeActivated)){
            getSushi = true;
        }
    }

    public void MoveSushi(){

        // Transform Sushi UI position to the world position
        sushiUIPosition = Camera.main.ScreenToWorldPoint(scoreManager.getSushiUI().transform.position);

        // Move current sushi to the position
        transform.position = Vector3.MoveTowards(transform.position, sushiUIPosition + Vector3.forward, 25 * Time.deltaTime);

        // When distance between sushi and UI less than 0.1, diactive the object
        if ((transform.position - (sushiUIPosition + Vector3.forward)).sqrMagnitude < 0.1f){
            getSushi = false;
            gameObject.SetActive(false);
            scoreManager.AddScore(scoreToGive);
        }
    }
}
