using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour{
    
    public MainMenu mainMenu;
    public  Text sushi;
    public Text score;
    public Text kan;
    public Text highestScore;
    public GameObject restartIcon;
    public GameObject mainMenuIcon;
    public AudioSource countSound;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public AudioSource buttonSound;

    private ScoreManager scoreManager;
    private GameManager gameManager;

    void Start(){
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();
    }   

    void Update(){
        score.text = scoreManager.scoreCount.ToString();
        highestScore.text = ("最高分: "+scoreManager.highScoreCount.ToString());
    }
    public void Restart(){
        gameManager.Reset();
        gameManager.StartGame();
        ResetAllIcons();
        gameObject.SetActive(false);
    }

    public void MainMenu(){
        mainMenu.gameObject.SetActive(true);
        gameManager.Reset();
        ResetAllIcons();
        gameObject.SetActive(false);
    }

    public void ShowFinalScore(){
        StartCoroutine("Counter");
    }

    public void ShowRestartIcon(){
        restartIcon.SetActive(true);
        buttonSound.Play();
    }

    public void HideRestartIcon(){
        restartIcon.SetActive(false);
    }

    public void ShowMainMenuIcon(){
        mainMenuIcon.SetActive(true);
        buttonSound.Play();
    }

    public void HideMainMenuIcon(){
        mainMenuIcon.SetActive(false);
    }

    private void ResetAllIcons(){
        restartIcon.SetActive(false);
        mainMenuIcon.SetActive(false);
        sushi.gameObject.SetActive(false);
        score.gameObject.SetActive(false);
        kan.gameObject.SetActive(false);
        highestScore.gameObject.SetActive(false);
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    private IEnumerator Counter(){
        float waitTime = 0.5f;
        yield return new WaitForSeconds(waitTime);
        sushi.gameObject.SetActive(true);
        countSound.Play();

        yield return new WaitForSeconds(waitTime);
        score.gameObject.SetActive(true);
        countSound.Play();

        yield return new WaitForSeconds(waitTime);
        kan.gameObject.SetActive(true);
        countSound.Play();

        yield return new WaitForSeconds(waitTime);
        highestScore.gameObject.SetActive(true);
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }
}
