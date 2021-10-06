using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int maxStability;
    [HideInInspector]
    public int id;
    public int devHighscore;
    public string levelName;

    public Player player;
    public AudioClip music;
    public Transform bossLava;
    public BeatRotator lavaRotator, bossRotator;


    [HideInInspector]
    public List<Atom> atoms;


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
        GM.I.gameplay.stability = maxStability;
        GM.I.gameplay.maxStability = maxStability;
        GM.I.gameplay.levelText.text = levelName;
        atoms.Clear();
        atoms.AddRange(GetComponentsInChildren<Atom>());
    }

    public void SpawnPlayer(){
        player.Initialize();

    }

    public void BossPhaseEnd(int id)
    {
        if(id == 0){
            StartCoroutine(BossLava());
        }
        else if(id == 1){
            lavaRotator.animTime = 4;
            bossRotator.animTime = 4;
        }else if (id == 2){
            GM.I.gameplay.WinGame();
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
