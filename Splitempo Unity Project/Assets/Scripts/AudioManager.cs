using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class _UnityEventFloat:UnityEvent<int> {}
public class AudioManager : MonoBehaviour
{
    public _UnityEventFloat onBeat;
    public float bpm, bpmInSeconds;
    public AudioSource musicSource;

    public double currentInterval = 0;
    public int beat;
    public int phrase;
    public int semiphrase;

    public AudioSource sfxSource;
    public AudioClip rise, crash, win;
    public AudioClip finalMusic;

    private void Start ()
    {
        bpmInSeconds = 30f / bpm;
        currentInterval = AudioSettings.dspTime + bpmInSeconds;
        musicSource.Play();
    }
 
    private void FixedUpdate ()
    {
        if (AudioSettings.dspTime >= currentInterval)
        {
            currentInterval += bpmInSeconds;
            beat ++;
            phrase ++;
            if(beat == 8){
                beat = 0;
            }
            if(phrase == 32){
                phrase = 0;
            }
            onBeat.Invoke(beat);
        }
    }

}
