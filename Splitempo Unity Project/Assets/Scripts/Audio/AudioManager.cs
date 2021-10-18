using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{

    [SerializeField] private float MusicVolumeMix;
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
    

    private void MusicSourceLerpedChange(float pitch, float volume, int duration)
    {
        StopCoroutine();
        musicSourceRoutine = StartCoroutine(MusicSourceLerpedChangeRoutine(pitch, volume, duration));
    }

    private void StopCoroutine()
    {
        if (musicSourceRoutine != null)
        {
            StopCoroutine(musicSourceRoutine);
        }
    }

    IEnumerator MusicSourceLerpedChangeRoutine(float pitch, float volume, int duration){
        float t = 0;
        float waitTime = BeatManager.BeatToSeconds(duration);
        float startPitch = _musicSource.pitch;
        float startVolume = _musicSource.volume;
        while(t < waitTime){
            _musicSource.pitch = Mathf.Lerp(startPitch, pitch, t/waitTime);
            _musicSource.volume = Mathf.Lerp(startVolume, volume * MusicVolumeMix, t/waitTime);
            t+= Time.deltaTime;
            yield return 0;
        }
        _musicSource.pitch = pitch;
        _musicSource.volume = volume;
    }

    


    public void SetMusicVolume(float targetValue){
        StopCoroutine();
        _musicSource.volume = targetValue * MusicVolumeMix;
    }

    public void SetMusicPitch(float targetValue)
    {
        StopCoroutine();
        _musicSource.pitch = targetValue;
    }

    public void StartMusic(AudioClip crash)
    {
        SetMusicVolume(1f);
        SetMusicPitch(1f);
        PlayMusicSFX(crash);
        _musicSource.Stop();
        _musicSource.Play();
    }

    public void StartWorldMusic(AudioClip music, float bpm)
    {
        if(music != null)
            _musicSource.clip = music;
        BeatManager.I.StartBeat(bpm);
        StartMusic(null);
    }
    public void StartNewMusic(AudioClip music, AudioClip crash)
    {
        if(music != null)
            _musicSource.clip = music;
        BeatManager.I.StartBeat();
        StartMusic(crash);
    }

    public void StartNewMusic(AudioClip music)
    {
        StartNewMusic(music, null);
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
        AudioSource.PlayClipAtPoint(sfx, point,10f);
    }

}
