using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour{
    public int moonChangeTime;
    public Sprite[] sprites;
    public int indexOfBloodMoon;

    private float moonChangeTimeCount;
    private int spriteIndex;
    private PlayerController player;
    private bool speedIncreasing;
    private bool boosting;

    // Start is called before the first frame update
    void Start(){
        StartCoroutine(Counter());
        player = FindObjectOfType<PlayerController>();
        reset();
    }

    // Update is called once per frame
    void Update(){

        // Change moon's sprite
        if(moonChangeTimeCount == 0){
            spriteIndex++;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex % sprites.Length];
            moonChangeTimeCount = moonChangeTime;
        }

        // If it's blood moon, boost player's speed and increase player's speed permanently after blood moon
        if(spriteIndex % sprites.Length == indexOfBloodMoon && !boosting){
            player.startTsuyoTsuyoMode();
            boosting = true;
            speedIncreasing = false;
        } else if (spriteIndex % sprites.Length == (indexOfBloodMoon + 1) % sprites.Length  && !speedIncreasing){
            player.SpeedUp();
            speedIncreasing = true;
            boosting = false;
        }
    }

    public void reset(){
        moonChangeTimeCount = moonChangeTime;
        speedIncreasing = true;
        boosting = false;
        spriteIndex = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex % sprites.Length];
    }

    private IEnumerator Counter(){
        while(moonChangeTimeCount >= 0){
            yield return new WaitForSeconds(1);
            moonChangeTimeCount--;
        }
    }
}
