
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string levelName;
    public int maxStability;
    public AudioClip music;
    [HideInInspector]
    public int id;
    public int devHighscore;

    public Player player;
    public Transform bossLava;
    public BeatRotator lavaRotator, bossRotator;


    [HideInInspector]
    public List<Atom> atoms;
    private bool _isPlaying;
    public bool IsPlaying => _isPlaying;

    public void Split(Atom parent, List<Atom> children) {
        if(parent != null){
            atoms.Remove(parent);
        }
        if(children != null){
            atoms.AddRange(children);
        }
    }

    public void Initialize(int i)
    {
        id = i;
        gameObject.SetActive(true);
        atoms.Clear();
        atoms.AddRange(GetComponentsInChildren<Atom>());
    }

    public void SpawnPlayer(){
        player.Initialize();
        _isPlaying = true;
    }

    public void StopLevel(){
        player.StopInput();
        _isPlaying = false;
    }
    public void BossPhaseEnd(int id)
    {
        if(id == 0){
            StartCoroutine(BossLava());
        }
        else if(id == 1){
            
        }else if (id == 2){
            GM.I.gp.WinGame();
        }
    }

    IEnumerator BossLava(){
        float t = 0;
        while (t < 1f)
        {
            bossLava.localScale = Vector3.Lerp(bossLava.localScale, Vector3.one, Time.deltaTime * 3f);
            t += Time.deltaTime;
            yield return 0;
        }
            bossLava.localScale = Vector3.one;
    }
}
