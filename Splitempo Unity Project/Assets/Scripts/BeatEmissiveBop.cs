using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEmissiveBop : BeatListener
{
    public Color startColor, targetColor;
    Material myMat;
    public float bopSpeed;
    private void Start() {
        myMat = GetComponent<MeshRenderer>().material;
        myMat.SetColor("_OutlineColor", Color.Lerp(myMat.GetColor("_OutlineColor"), startColor, 1));
    }
    public override void OnBeat()
    {
        if(!GM.I.VFXMute){
            myMat.SetColor("_OutlineColor", targetColor);
        }
        base.OnBeat();
    }

    // Update is called once per frame
    void Update()
    {
        myMat.SetColor("_OutlineColor", Color.Lerp(myMat.GetColor("_OutlineColor"), startColor, Time.deltaTime * bopSpeed));
    }
}
