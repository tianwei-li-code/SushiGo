using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScrennToggle : MonoBehaviour{
    public GameObject icon;
    public Sprite fullScreenIcon;
    public Sprite windowedIcon;

    private int fullScreenStatus;

    void Start(){
        fullScreenStatus = PlayerPrefs.HasKey("FullScreen") ? PlayerPrefs.GetInt("FullScreen") : 0;
        icon.GetComponent<Image>().sprite = fullScreenStatus == 1 ? windowedIcon : fullScreenIcon;
        if(fullScreenStatus == 1){
            gameObject.GetComponent<Toggle>().isOn = true;
        }
    }
    
    public void FullScreen(bool isFullScreen){
        fullScreenStatus = isFullScreen ? 1 : 0;
        Screen.fullScreenMode = isFullScreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
        icon.GetComponent<Image>().sprite = isFullScreen ? windowedIcon : fullScreenIcon;
        if(isFullScreen){
            Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
        }

        PlayerPrefs.SetInt("FullScreen", fullScreenStatus);
    }
}
