using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour{
    
    public MainMenu mainMenu;
    public Text score;
    public Text highestScore;
    public GameObject restartIcon;
    public GameObject mainMenuIcon;

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

    public void ShowRestartIcon(){
        restartIcon.SetActive(true);
    }

    public void HideRestartIcon(){
        restartIcon.SetActive(false);
    }

    public void ShowMainMenuIcon(){
        mainMenuIcon.SetActive(true);
    }

    public void HideMainMenuIcon(){
        mainMenuIcon.SetActive(false);
    }

    private void ResetAllIcons(){
        restartIcon.SetActive(false);
        mainMenuIcon.SetActive(false);
    }
}
