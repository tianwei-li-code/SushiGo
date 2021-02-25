using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public CreditMenu creditMenu;
    public GameObject startIcon;
    public GameObject creditIcon;
    public GameObject quitIcon;
    
    private GameManager gameManager;

    void Start(){
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void PlayGame(){
        gameManager.StartGame();
        ResetAllIcons();
        gameObject.SetActive(false);
    }

    public void Credit(){
        creditMenu.gameObject.SetActive(true);
        ResetAllIcons();
        gameObject.SetActive(false);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void ShowStartIcon(){
        startIcon.SetActive(true);
    }

    public void HideStartIcon(){
        startIcon.SetActive(false);
    }

    public void ShowCreditIcon(){
        creditIcon.SetActive(true);
    }

    public void HideCreditIcon(){
        creditIcon.SetActive(false);
    }

    public void ShowQuitIcon(){
        quitIcon.SetActive(true);
    }

    public void HideQuitIcon(){
        quitIcon.SetActive(false);
    }

    private void ResetAllIcons(){
        startIcon.SetActive(false);
        creditIcon.SetActive(false);
        quitIcon.SetActive(false);
    }

    
}
