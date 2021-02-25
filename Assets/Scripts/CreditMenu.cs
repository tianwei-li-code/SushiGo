using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : MonoBehaviour{
    public MainMenu mainMenu;
    public void MainMenu(){
        mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
