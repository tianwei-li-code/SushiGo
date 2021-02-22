using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEliminator : MonoBehaviour{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Mana"){
            gameObject.SetActive(false);
        }
    }

}
