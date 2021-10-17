using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    static GM _instance;
    public static GM I {
        get {
            if (!_instance) {
                _instance = FindObjectOfType<GM>();
            }
            return _instance;
        }
    }

    public AudioManager am;
    public CameraManager cam;
    public GameplayManager gp;

    public bool MusicMute = false;
    public bool SFXMute = false;
    public bool VFXMute = false;

    public List<string> trophyLevels;
    public int trophies {get{return trophyLevels.Count;}}

    public void ToggleMusic(){
        MusicMute = !MusicMute;
    }

    public void ToggleSFX(){
        SFXMute = !SFXMute;
    }

    public void ToggleVFX(){
        VFXMute = !VFXMute;
    }

    public void StartGame(int id){
        gameObject.SetActive(true);
        StartCoroutine(StartGameRoutine(id));
    }

    public void Quit(){
        Application.Quit();
    }

    IEnumerator StartGameRoutine(int id)
    {
        AudioManager.I.LerpMusicVolume(0f, BeatManager.TimeUntilBeatInPhrase(16));
        yield return BeatManager.WaitUntilBeatInPhrase(16);
        while (BeatManager.I.CurrentBeatInPhrase != 16)
        {
            
            yield return null;
        }
        AudioManager.I.SetMusicVolume(1f);
        AudioManager.I.StartNewMusic(gp.CurrentLevel.music, gp.CurrentWorld.sfxCrash);
        gp.SpawnLevel(id);
        gp.CurrentLevel.SpawnPlayer();
        
    }

    public void Restart(){
        SceneManager.LoadScene(0);
    }

}
