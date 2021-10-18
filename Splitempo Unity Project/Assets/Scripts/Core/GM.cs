using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : SingletonBase<GM>
{

    public GameplayManager gp;
    public WorldManager world;
    public PlayerSave save;
    public MenuUIManager menuUIManager;

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
        menuUIManager.Hide();
        AudioManager.I.LerpMusicVolume(0f, BeatManager.TimeUntilBeatInPhrase(16));
        gp.SpawnLevel(id);
        yield return BeatManager.WaitUntilBeatInPhrase(0);
        AudioManager.I.StartNewMusic(gp.CurrentLevel.music, gp.CurrentWorld.sfxCrash);
        gp.CurrentLevel.SpawnPlayer();
        
    }

    public void Restart(){
        SceneManager.LoadScene(0);
    }

}
