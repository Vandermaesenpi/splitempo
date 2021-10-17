using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BeatManager : MonoBehaviour
{
    #region singleton pattern
    private static BeatManager _instance;
    public static BeatManager I 
    {
        get {
            if (!_instance) {
                _instance = FindObjectOfType<BeatManager>();
            }
            return _instance;
        }
    }

    private void Awake() {
        _instance = this;
    }
    #endregion

    #region public getters
    public int CurrentBeatInBar => _currentBeat % 8;
    public int CurrentBeatInPhrase => _currentBeat % 32;
    public float CurrentBPM => _beatData.bpm;
    public float CurrentBPMInSeconds => _bpmInSeconds;
    private bool NewBeatCrossed => AudioSettings.dspTime >= _currentInterval;
    #endregion

    #region variables
    public UnityEvent onBeat;
    private BeatData _beatData;
    private float _bpmInSeconds;
    private double _currentInterval = 0;
    private int _currentBeat;
    #endregion

    #region main loop
    private void FixedUpdate ()
    {
        if (NewBeatCrossed)
        {
            UpdateBeatClocks();
            onBeat.Invoke();
        }
    }

    #endregion

    #region methods
    public void StartBeat(){
        StartBeat(_beatData);
    }
    public void StartBeat(BeatData _data)
    {
        _beatData = _data;
        _bpmInSeconds = 30f / _beatData.bpm;
        _currentInterval = AudioSettings.dspTime + _bpmInSeconds;
        _currentBeat = 0;
    }
    private void UpdateBeatClocks()
    {
        _currentInterval += _bpmInSeconds;
        _currentBeat++;
    }

    public static float BeatToSeconds(int beatLength) => (float)beatLength * I.CurrentBPMInSeconds;
    public static int TimeUntilBeatInPhrase(int targetBeat)
    {
        if(I.CurrentBeatInPhrase == targetBeat){
            return 32;
        }

        if(I.CurrentBeatInPhrase > targetBeat){
            return 32 + targetBeat - I.CurrentBeatInPhrase;
        }

        return I.CurrentBeatInPhrase - targetBeat;
    }

    public static IEnumerator WaitUntilBeatInBar(int beat)
    {
        while(I.CurrentBeatInBar != beat){
            yield return 0;
        }
    }

    public static IEnumerator WaitUntilBeatInPhrase(int beat)
    {
        while(I.CurrentBeatInPhrase != beat){
            yield return 0;
        }
    }

    public static IEnumerator WaitForBeatDuration(int duration)
    {
        int targetBeat = I._currentBeat + duration; 
        while(I._currentBeat < targetBeat){
            yield return 0;
        }
    }

    #endregion
}
