using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour{
    public float moonChangeTime;
    public Sprite[] sprites;
    public int indexOfBloodMoon;
    public int indexOfDarkMoon;
    public BackgroundController background;

    private float moonChangeTimeCount;
    private float mireiTimeCounter;
    private int spriteIndex;
    private PlayerController player;
    private MireiEyeGenerator mireiEyeGenerator;
    private bool speedIncreasing;
    private bool boosting;
    private bool mireiTime;

    // Start is called before the first frame update
    void Start(){
        StartCoroutine(Counter());
        player = FindObjectOfType<PlayerController>();
        mireiEyeGenerator = FindObjectOfType<MireiEyeGenerator>();
        Reset();
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
            background.BloodSky();
        } else if (spriteIndex % sprites.Length == (indexOfBloodMoon + 1) % sprites.Length  && !speedIncreasing){
            player.SpeedUp();
            speedIncreasing = true;
            boosting = false;
            background.NormalSky();
        }

        // If it's dark moon, generate mirei eye
        if(spriteIndex % sprites.Length == indexOfDarkMoon && !mireiTime){
            mireiTime = true;
            StartCoroutine("MireiTime");
        } else if (spriteIndex % sprites.Length == (indexOfDarkMoon + 1) % sprites.Length){
            mireiTime = false;
        }
    }

    public void Reset(){
        moonChangeTimeCount = moonChangeTime;
        speedIncreasing = true;
        boosting = false;
        mireiTime = false;
        spriteIndex = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[spriteIndex % sprites.Length];
        background.NormalSky();
        StopCoroutine("MireiTime");
    }

    private IEnumerator Counter(){
        while(moonChangeTimeCount >= 0){
            yield return new WaitForSeconds(1);
            moonChangeTimeCount--;
        }
    }

    private IEnumerator MireiTime(){
        mireiTimeCounter = Random.Range(0.5f, moonChangeTime);
        while(mireiTimeCounter > 0){
            yield return new WaitForSeconds(0.1f);
            mireiTimeCounter-=0.1f;
            if(mireiTimeCounter <= 0){
                mireiEyeGenerator.GenerateMireiEye();
            }
        }
    }
}
