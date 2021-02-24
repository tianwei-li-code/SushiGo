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
    private ScoreManager scoreManager;
    private MoonController moonController;
    private PlatformDestroyer[] items;
    private BuffGenerator buffGenerator;

    // Start is called before the first frame update
    void Start(){
        platformStartPoint = platformGenerator.position;
        itemStartPoint = itemGenerator.position;
        playerStarterPoint = player.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        moonController = FindObjectOfType<MoonController>();
        buffGenerator = FindObjectOfType<BuffGenerator>();
    }

    // Update is called once per frame
    void Update(){
        
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

        // Reset the score
        scoreManager.scoreCount = 0;

        // Reset the moon
        moonController.Reset();

        // Reset the buff generator
        buffGenerator.Reset();
    }
}
