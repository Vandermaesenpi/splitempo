using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region singleton pattern
    private static AudioManager _instance;
    public static AudioManager I 
    {
        get {
            if (!_instance) {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }

    private void Awake() {
        _instance = this;
    }
    #endregion
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    Coroutine musicSourceRoutine;

    private void Start() {
        _musicSource.Play();
    }

    public void LerpMusicPitch(float targetValue, int duration)
    {
        MusicSourceLerpedChange(targetValue, _musicSource.volume, duration);
    }
    public void LerpMusicVolume(float targetValue, int duration){
        MusicSourceLerpedChange(_musicSource.pitch, targetValue, duration);
    }

    public void LerpMusicVolume(float targetPitchValue,float targetVolumeValue, int duration){
        MusicSourceLerpedChange(targetPitchValue, targetVolumeValue, duration);
    }
    

    private void MusicSourceLerpedChange(float pitch, float volume, int duration){
        
        if(musicSourceRoutine != null){
            StopCoroutine(musicSourceRoutine);
        }
        musicSourceRoutine = StartCoroutine(MusicSourceLerpedChangeRoutine(pitch, volume, duration));
    }

    IEnumerator MusicSourceLerpedChangeRoutine(float pitch, float volume, int duration){
        float t = 0;
        float waitTime = BeatManager.BeatToSeconds(duration);
        while(t < waitTime){
            _musicSource.pitch = Mathf.Lerp(_musicSource.pitch, pitch, t/waitTime);
            _musicSource.volume = Mathf.Lerp(_musicSource.volume, volume, t/waitTime);
            t+= Time.deltaTime;
            yield return 0;
        }
        _musicSource.pitch = pitch;
        _musicSource.volume = volume;
    }

    


    public void SetMusicVolume(float targetValue){
        _musicSource.volume = targetValue;
    }

    public void SetMusicPitch(float targetValue)
    {
        _musicSource.pitch = targetValue;
    }

    public void StartMusic(AudioClip crash)
    {
        SetMusicVolume(1f);
        SetMusicPitch(1f);
        PlayMusicSFX(crash);
        BeatManager.I.StartBeat();
        _musicSource.Stop();
        _musicSource.Play();
    }

    public void StartNewMusic(AudioClip music, AudioClip crash)
    {
        _musicSource.clip = music;
        StartMusic(crash);
    }

    public void StartNewMusic(AudioClip music)
    {
        _musicSource.clip = music;
        StartMusic(null);
    }

    public void PlayMusicSFX(AudioClip sfx)
    {
        if(sfx != null){
            _sfxSource.PlayOneShot(sfx);
        }
    }

    public static void PlaySFX(AudioClip sfx){
        AudioManager.PlaySFX(sfx, Vector3.zero);
    }

    public static void PlaySFX(AudioClip sfx, Vector3 point){
        AudioSource.PlayClipAtPoint(sfx, point);
    }
}
