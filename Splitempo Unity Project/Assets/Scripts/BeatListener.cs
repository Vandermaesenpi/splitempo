using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatListener : MonoBehaviour
{
    public BeatListener refBeat;
    public bool[] beats = new bool[8];
    private void OnEnable() {
        if(refBeat != null){
            beats = refBeat.beats;
        }
        GM.I.am.onBeat.AddListener(CheckBeat);
    }

    private void OnDisable() {
        if(GM.I != null)
        GM.I.am.onBeat.RemoveListener(CheckBeat);
    }

    public void CheckBeat(int i){
        if(beats[i]){
            OnBeat();
        }
    }

    public virtual void OnBeat(){
    }

    
}
