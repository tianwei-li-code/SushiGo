using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MireiEyeGenerator : MonoBehaviour{
    public GameObject eyeGenerationPoint;

    private MireiEyeController mireiEye;

    // Start is called before the first frame update
    void Start(){
        mireiEye = FindObjectOfType<MireiEyeController>();
    }

    public void GenerateMireiEye(){
        mireiEye.transform.position = eyeGenerationPoint.transform.position;
        mireiEye.Reset();
    }
}
