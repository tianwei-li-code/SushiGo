using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : MonoBehaviour{
    public MainMenu mainMenu;
    public GameObject mainMenuIcon;
    public AudioSource buttonSound;

    public void MainMenu(){
        mainMenu.gameObject.SetActive(true);
        ResetAllIcons();
        gameObject.SetActive(false);
    }

    public void ShowMainMenuIcon(){
        mainMenuIcon.SetActive(true);
        buttonSound.Play();
    }

    public void HideMainMenuIcon(){
        mainMenuIcon.SetActive(false);
    }

    private void ResetAllIcons(){
        mainMenuIcon.SetActive(false);
    }
}
