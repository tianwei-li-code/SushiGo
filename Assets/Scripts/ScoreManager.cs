using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour{
    public Text scoreText;
    public int scoreCount;
    public int highScoreCount;
    public GameObject sushiUI;

    // Start is called before the first frame update
    void Start(){
        if(PlayerPrefs.HasKey("HighScore")){
            highScoreCount = PlayerPrefs.GetInt("HighScore");
        }
    }

    // Update is called once per frame
    void Update(){
        if(scoreCount > highScoreCount){
            highScoreCount = scoreCount;
            PlayerPrefs.SetInt("HighScore", highScoreCount);
        }

        scoreText.text = "X " + scoreCount;
    }

    public void AddScore(int points){
        scoreCount += points;
    }

    public GameObject getSushiUI(){
        return sushiUI;
    }
}
