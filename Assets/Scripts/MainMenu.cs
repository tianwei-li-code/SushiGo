using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public CreditMenu creditMenu;
    
    private GameManager gameManager;

    void Start(){
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void PlayGame(){
        gameManager.StartGame();
        gameObject.SetActive(false);
    }

    public void Credit(){
        creditMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
