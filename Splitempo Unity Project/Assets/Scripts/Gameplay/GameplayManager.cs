using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    private StabilityManager _stabilityManager;
    public UnityEventIntFloat onStabilityChanged;
    public UnityEventInt onShotsChanged;
    public UnityEventInt onComboChanged;

    private GameUIManager _gameUIManager;

    public LevelManager CurrentLevel => GM.I.world.currentLevel;
    public World CurrentWorld => GM.I.world.currentWorld;
    private ComboManager _comboManager;

    public bool won;
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
        _comboManager = GetComponent<ComboManager>();
        _comboManager.SetManager(this);
    }

    public void Bump(){

    }


    public void Hurt(){
        if(dead || won){return;}
        _stabilityManager.DecreaseStability(10);
        if(!dead){
            CameraManager.I.StartPostProcessingEffect(PostProcessEffectType.Hurt);
        }
    }


    public void SpawnLevel(int id){
        if(CurrentLevel != null){Destroy(CurrentLevel.gameObject);}
        gameObject.SetActive(true);
        shotsTaken = 0;
        dead = false;
        won = false;
        GM.I.world.SpawnLevel(id);
        _stabilityManager.Initialize(CurrentLevel.maxStability);
        levelText.text = CurrentLevel.levelName;
    }

    Coroutine endLevelRoutine;

    public void LooseLevel()
    {
        if(won){return;}
        dead = true;
        if(endLevelRoutine != null){
            StopCoroutine(endLevelRoutine);
        }
        endLevelRoutine = StartCoroutine(RestartRoutine());
    }

    public IEnumerator RestartRoutine()
    {

        CameraManager.I.StartPostProcessingEffect(PostProcessEffectType.Loose);
        CurrentLevel.DestroyBalls();
        CurrentLevel.StopLevel();
        AudioManager.I.LerpMusicPitch(0f, 16);
        yield return StartCoroutine(BeatManager.WaitForBeatDuration(2));
        yield return StartCoroutine(_transitionScreen.StartTransition());
        RespawnLevel();
        CameraManager.I.StartPostProcessingEffect(PostProcessEffectType.None);
        AudioManager.I.LerpMusicPitch(0.5f, 8);
        yield return StartCoroutine(_transitionScreen.ShowRebootUI());
        AudioManager.I.LerpMusicVolume(0f,4);
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

    public void PreSplit(Atom atom, int childrenCount) {
        _comboManager.AddToCombo(atom, childrenCount);
    }

    public void CheckWinState(){
        StartCoroutine(CheckWinStateRoutine());
    }

    IEnumerator CheckWinStateRoutine(){
        yield return 0;
        yield return 0;
        if(CurrentLevel.NoMoreAtoms){
            WinLevel();
        }
    }



    public void WinLevel(){
        won = true;
        if(endLevelRoutine != null){
            StopCoroutine(endLevelRoutine);
        }
        endLevelRoutine = StartCoroutine(WinLevelRoutine());
    }

    public IEnumerator WinLevelRoutine (){
        
        // Grace period, to sync with the beat
        CameraManager.I.StartPostProcessingEffect(PostProcessEffectType.Win);
        AudioManager.I.PlayMusicSFX(CurrentWorld.sfxWinLevel);
        CurrentLevel.StopLevel();
        
        yield return StartCoroutine(BeatManager.WaitUntilBeatInPhrase(0));
        AudioManager.I.PlayMusicSFX(CurrentWorld.sfxRise);
        yield return StartCoroutine(_transitionScreen.StartTransition());
        SpawnNextLevel();
        CameraManager.I.StartPostProcessingEffect(PostProcessEffectType.None);
        yield return StartCoroutine(_transitionScreen.ShowNextLevelUI());
        AudioManager.I.LerpMusicVolume(0f,4);
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
        CameraManager.I.StartPostProcessingEffect(PostProcessEffectType.BossHurt);

        yield return StartCoroutine(BeatManager.WaitUntilBeatInPhrase(0));

        CurrentLevel.gameObject.SetActive(false);
        endgameMenu.SetActive(true);
        AudioManager.I.StartNewMusic(CurrentWorld.sfxWinWorld);
        CameraManager.I.StartPostProcessingEffect(PostProcessEffectType.None);
        gameObject.SetActive(false);
        
    }

}
