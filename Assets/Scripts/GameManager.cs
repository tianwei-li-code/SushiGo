using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public DeathMenu deathMenu;
    public int gameStartWaitingTime;
    private int startWaitingTimeCounter;
    public GameObject countdown;
    public GameObject scoreCount;
    public AudioSource bgm;
    private float bgmVolume;
    public AudioSource countdownSound;
    public AudioSource countdownEndSound;
    

    // Start is called before the first frame update
    void Start(){
        platformStartPoint = platformGenerator.position;
        itemStartPoint = itemGenerator.position;
        playerStarterPoint = player.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        moonController = FindObjectOfType<MoonController>();
        buffGenerator = FindObjectOfType<BuffGenerator>();
        bgmVolume = bgm.volume;
    }

    // Update is called once per frame
    void Update(){
        
    }

    // Game start
    public void StartGame(){

        // Activate score
        scoreCount.SetActive(true);

        // Start countdown
        StartCoroutine("Countdown");
    }

    // After player dies, activate death menu and stop all Coroutine
    public void RestartGame(){

        // Deactivate player
        player.gameObject.SetActive(false);

        // Activate death menu
        deathMenu.gameObject.SetActive(true);
        deathMenu.ShowFinalScore();

        // Deactivate score
        scoreCount.SetActive(false);

        // Stop all Coroutines
        moonController.Pause();
        buffGenerator.Pause();

        // Stop BGM
        StartCoroutine("FadeOutBGM");
    }

    // Reset game
    public void Reset(){

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

    private IEnumerator Countdown(){
        startWaitingTimeCounter = gameStartWaitingTime;
        Text t = countdown.GetComponent<Text>();
        t.color = new Color(t.color.r, t.color.g, t.color.b, 1);
        countdown.SetActive(true);

        while(startWaitingTimeCounter > 0){
            // Change countdown text
            t.text = startWaitingTimeCounter.ToString();

            // Play countdown sound
            countdownSound.Play();

            yield return new WaitForSeconds(1);
            startWaitingTimeCounter--;

            // When countdown over
            if(startWaitingTimeCounter <= 0){

                // Player start moving
                player.stopMoving = false;

                // Start all Coroutines
                moonController.StartCounter();
                buffGenerator.StartCounter();

                // Fade out GO
                t.text = "GO";
                StartCoroutine("FadeOutText");

                // Play countdown end sound
                countdownEndSound.Play();

                // Play BGM
                bgm.Play();
                StartCoroutine("FadeInBGM");
            }
        }
    }

    private IEnumerator FadeOutText(){
        Text t = countdown.GetComponent<Text>();
        float fadeOutTime = 2.5f;
        while(t.color.a > 0f){
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a - (Time.deltaTime / fadeOutTime));
            yield return null;
        }

        // Deactivate count down
        countdown.SetActive(false);
    }

    private IEnumerator FadeOutBGM(){
        float fadeOutTime = 2.5f;
        while(bgm.volume > 0f){
            bgm.volume -= bgmVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        // Stop BGM
        bgm.Stop();
    }

    private IEnumerator FadeInBGM(){
        float fadeInTime = 2.5f;
        bgm.volume = 0;
        while(bgm.volume < bgmVolume){
            bgm.volume += bgmVolume/( fadeInTime/Time.deltaTime);
            yield return null;
        }
    }
}
