using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatListener : MonoBehaviour
{
    public Sequence _sequencerPattern;
    [SerializeField] private BeatListener _referenceBeat;
    private void OnEnable() {
        _referenceBeat?.GiveSequencerPatternTo(this);
        BeatManager.I.onBeat.AddListener(CheckBeat);
    }

    private void GiveSequencerPatternTo(BeatListener beatListener)
    {
        beatListener._sequencerPattern = _sequencerPattern;
    }

    private void OnDisable() {
        BeatManager.I?.onBeat.RemoveListener(CheckBeat);
    }

    private void CheckBeat(){
        if(_sequencerPattern.HasNoteThisBeat){
            OnNotePlay();
        }
    }

    public virtual void OnNotePlay(){}

    
}
