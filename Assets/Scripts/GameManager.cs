using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public Transform platformGenerator;
    private Vector3 platformStartPoint;
    public Transform itemGenerator;
    private Vector3 itemStartPoint;
    public PlayerController player;
    private Vector3 playerStarterPoint;

    public int speedIncreaseTime;
    private float speedIncreaseTimeCount;
    private PlatformDestroyer[] items;

    // Start is called before the first frame update
    void Start(){
        platformStartPoint = platformGenerator.position;
        itemStartPoint = itemGenerator.position;
        playerStarterPoint = player.transform.position;

        speedIncreaseTimeCount = speedIncreaseTime;
        StartCoroutine(Time());
    }

    // Update is called once per frame
    void Update(){

        // Increase player's speed
        if(speedIncreaseTimeCount == 0){
            player.SpeedUp();
            speedIncreaseTimeCount = speedIncreaseTime;
        }
    }

    // Reset the game
    public void RestartGame(){
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo(){
        yield return new WaitForSeconds(0.8f);
        player.gameObject.SetActive(false);

        // Destroy all items who have PlatformDestroyer Script
        items = FindObjectsOfType<PlatformDestroyer>();
        for(int i = 0; i < items.Length; i++){
            items[i].gameObject.SetActive(false);
        }

        // Reset position
        player.transform.position = playerStarterPoint;
        platformGenerator.position = platformStartPoint;
        itemGenerator.position = itemStartPoint;
        player.gameObject.SetActive(true);
        player.Respawn();

        // Reset speed increase time counter
        speedIncreaseTimeCount = speedIncreaseTime;
    }

    // Moon change time countdown/ Speed up time count down
    private IEnumerator Time(){
        while(speedIncreaseTimeCount >= 0){
            yield return new WaitForSeconds(1);
            speedIncreaseTimeCount--;
        }
    }
}
