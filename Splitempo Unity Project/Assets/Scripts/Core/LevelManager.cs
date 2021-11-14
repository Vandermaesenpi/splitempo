
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<Atom> _atoms;
    private bool _isPlaying;
    public bool IsPlaying => _isPlaying;

    private int waitingChildrenCount;
    internal bool NoMoreAtoms {
        get{
            Debug.Log("ATOM COUNTS = " + _atoms.Count);
            return _atoms.Count == 0 && waitingChildrenCount == 0;

        }
    } 
    public  void SplitAtom(Atom atom)
    {
        SplitAtom(atom, 0);
    }
    public void SplitAtom(Atom parent, int children) {
        if(parent != null){
            _atoms.Remove(parent);
        }
        if(children > 0){
            waitingChildrenCount += children;
        }
    }

    public void AddNewAtoms(List<Atom> atoms){
        _atoms.AddRange(atoms);
    }

    public void AddNewWaitingAtoms(List<Atom> atoms){
        waitingChildrenCount -= atoms.Count;
        AddNewAtoms(atoms);
    }


    public void Initialize(int i)
    {
        waitingChildrenCount = 0;
        id = i;
        gameObject.SetActive(true);
        _atoms.Clear();
        _atoms.AddRange(GetComponentsInChildren<Atom>().ToList());
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

    internal void DestroyBalls()
    {
        player.DestroyBalls();
    }
}
