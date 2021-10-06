using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatTextColorBop : BeatListener
{
    public Color startColor, targetColor;
    Text myMat;
    public float bopSpeed;
    private void Start() {
        myMat = GetComponent<Text>();
        myMat.color =  Color.Lerp(myMat.color, startColor, 1);
    }
    public override void OnBeat()
    {
        if(!GM.I.VFXMute){
            myMat.color = targetColor;
        }
        base.OnBeat();
    }

    // Update is called once per frame
    void Update()
    {
        myMat.color =  Color.Lerp(myMat.color, startColor, Time.deltaTime * bopSpeed);
    }
}
