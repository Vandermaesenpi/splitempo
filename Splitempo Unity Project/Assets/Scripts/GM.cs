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
    public GameUIManager ui;

    public bool MusicMute = false;
    public bool SFXMute = false;
    public bool VFXMute = false;

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
        if(GM.I.cam.volumeStatusRout != null){
            StopCoroutine(GM.I.cam.volumeStatusRout);
        }

        while (GM.I.am.phrase != 16)
        {
            GM.I.cam.statusVolumes[5].weight += Time.deltaTime * 0.1f;
            //currentLevel.transform.localScale *= 0.999f;
            GM.I.am.musicSource.volume -= Time.deltaTime * 0.1f;
            yield return null;
        }
        GM.I.cam.statusVolumes[5].weight = 0f;;
        GM.I.am.musicSource.volume = 1f;
        GM.I.am.sfxSource.PlayOneShot(GM.I.gp.currentWorld.crash);
        gp.SpawnLevel(id);
        gp.currentLevel.SpawnPlayer();
        if(gp.currentLevel.music != null){
            GM.I.am.musicSource.Stop();
            GM.I.am.musicSource.clip = gp.currentLevel.music;
            GM.I.am.musicSource.Play();
        }
        
    }

    public void Restart(){
        SceneManager.LoadScene(0);
    }

}
