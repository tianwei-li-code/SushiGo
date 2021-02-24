using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour{
    public Sprite normalSky;
    public Sprite bloodSky;
    
    public void NormalSky(){
        gameObject.GetComponent<SpriteRenderer>().sprite = normalSky;
    }

    public void BloodSky(){
        gameObject.GetComponent<SpriteRenderer>().sprite = bloodSky;
    }
}
