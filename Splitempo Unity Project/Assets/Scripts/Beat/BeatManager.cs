using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BeatManager : SingletonBase<BeatManager>
{

    #region public getters
    public int CurrentBeatInBar => _currentBeat % 8;
    public int CurrentBeatInPhrase => _currentBeat % 16;
    public float CurrentBPM => _beatData;
    public float CurrentBPMInSeconds => _bpmInSeconds;
    private bool NewBeatCrossed => AudioSettings.dspTime >= _currentInterval;
    #endregion

    #region variables
    public UnityEvent onBeat;
    private float _beatData;
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
    public void StartBeat(float _data)
    {
        _beatData = _data;
        _bpmInSeconds = 30f / _beatData;
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
            return 16;
        }

        if(I.CurrentBeatInPhrase > targetBeat){
            return 16 + targetBeat - I.CurrentBeatInPhrase;
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
