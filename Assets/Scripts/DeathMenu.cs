using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour{
    
    public MainMenu mainMenu;
    public Text score;
    public Text highestScore;

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
    }

    public void MainMenu(){
        mainMenu.gameObject.SetActive(true);
        gameManager.Reset();
        gameObject.SetActive(false);
    }
}
