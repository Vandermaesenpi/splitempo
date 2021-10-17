using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    private StabilityManager _stabilityManager;
    public UnityEvent<int,float> onStabilityChanged;
    public UnityEvent<int> onShotsChanged;

    private GameUIManager _gameUIManager;

    private WorldManager _worldManager;
    public LevelManager CurrentLevel => _worldManager.currentLevel;
    public World CurrentWorld => _worldManager.currentWorld;


    public int shotsTaken;
    public bool startFromLast = false;

    public Transform transitionObject;
    [SerializeField] private TransitionScreen _transitionScreen;

    public Text levelText;
    public GameObject endgameMenu;

    public void ShotTaken()
    {
        shotsTaken ++;
        onShotsChanged.Invoke(shotsTaken);
    }

    public bool dead = true;

    private void Awake() {
        _stabilityManager = GetComponent<StabilityManager>();
        _stabilityManager.SetManager(this);
        _gameUIManager = GetComponent<GameUIManager>();
        _gameUIManager.SetManager(this);
    }

    public void Bump(){

    }


    public void Hurt(){
        if(dead){return;}
        _stabilityManager.DecreaseStability(10);
        GM.I.cam.StartPostProcessingEffect(PostProcessEffectType.Hurt);
    }


    public void SpawnLevel(int id){
        if(CurrentLevel != null){Destroy(CurrentLevel.gameObject);}
        gameObject.SetActive(true);
        shotsTaken = 0;
        dead = false;
        _worldManager.SpawnLevel(id);
        _stabilityManager.Initialize(CurrentLevel.maxStability);
        levelText.text = CurrentLevel.levelName;
    }

    public void LooseLevel()
    {
        StartCoroutine(Restart());
        
    }

    public IEnumerator Restart()
    {

        GM.I.cam.StartPostProcessingEffect(PostProcessEffectType.Loose);
        CurrentLevel.StopLevel();
        AudioManager.I.LerpMusicPitch(0f, 8);
        yield return StartCoroutine(BeatManager.WaitForBeatDuration(2));
        yield return StartCoroutine(_transitionScreen.StartTransition());
        RespawnLevel();
        GM.I.cam.StartPostProcessingEffect(PostProcessEffectType.None);
        yield return StartCoroutine(_transitionScreen.ShowRebootUI());
        yield return StartCoroutine(_transitionScreen.EndTransition());
        AudioManager.I.StartMusic(GM.I.gp.CurrentWorld.sfxCrash);
        CurrentLevel.SpawnPlayer();
    }

    private void SpawnNextLevel()
    {
        SpawnLevel(CurrentLevel.id+1);
    }

    private void RespawnLevel()
    {
        SpawnLevel(CurrentLevel.id);
    }

    public void Split(Atom parent, List<Atom> children) {
        CurrentLevel.Split(parent, children);
        if(CurrentLevel.atoms.Count == 0){
            WinLevel();
        }
    }



    public void WinLevel(){
        StartCoroutine(Transition());
    }
    



    public IEnumerator Transition (){
        
        // Grace period, to sync with the beat
        GM.I.cam.StartPostProcessingEffect(PostProcessEffectType.Win);
        AudioManager.I.PlayMusicSFX(CurrentWorld.sfxWinLevel);
        CurrentLevel.StopLevel();
        
        yield return StartCoroutine(BeatManager.WaitUntilBeatInPhrase(0));
        AudioManager.I.PlayMusicSFX(CurrentWorld.sfxRise);
        yield return StartCoroutine(_transitionScreen.StartTransition());
        SpawnNextLevel();
        GM.I.cam.StartPostProcessingEffect(PostProcessEffectType.None);
        yield return StartCoroutine(_transitionScreen.ShowNextLevelUI());
        yield return StartCoroutine(_transitionScreen.EndTransition());
        AudioManager.I.StartNewMusic(CurrentLevel.music, CurrentWorld.sfxCrash);
        CurrentLevel.SpawnPlayer();
    }

    

    public void WinGame()
    {
        StartCoroutine(WinGameRoutine());   
    }

    public IEnumerator WinGameRoutine (){
        
        AudioManager.I.SetMusicVolume(0);
        CurrentLevel.player.StopInput();
        GM.I.cam.StartPostProcessingEffect(PostProcessEffectType.BossHurt);

        yield return StartCoroutine(BeatManager.WaitUntilBeatInPhrase(16));

        CurrentLevel.gameObject.SetActive(false);
        endgameMenu.SetActive(true);
        AudioManager.I.StartNewMusic(CurrentWorld.sfxWinWorld);
        GM.I.cam.StartPostProcessingEffect(PostProcessEffectType.None);
        gameObject.SetActive(false);
        
    }

}
