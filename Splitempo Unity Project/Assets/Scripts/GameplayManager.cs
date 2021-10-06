using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gpManager : BeatListener
{
    public float stability = 100f;
    public float maxStability = 100f;

    public int shotsTaken = 0;
    public bool shot = false;
    public bool startFromLast = false;

    public List<World> worlds;
    public World currentWorld;
    List<Level> levels;
    public Level currentLevel;
    public Transform transitionObject;
    public float xOffset, lerpSpeed;
    public Text transitionText, scoreText, highscoreText;
    public Image logo;
    public Sprite logoGood, logoBad;
    public Text levelText;
    public GameObject endgameMenu;
    
    public bool dead = true;
    private void Start() {
        
    }

    

    public void Bump(){

    }

    public override void OnBeat()
    {
        if(currentLevel.player.playerBalls[0].interactable){
            stability--;
            if(stability <= 0){
            LooseLevel();
            }
        }
    }

    public void Hurt(){
        if(dead){return;}
        stability -= 10;
        if(stability <= 0){
            LooseLevel();
        }else{
            GM.I.cam.SetVolumeStatus(1);
        }

    }

    public void WinGame()
    {
        Debug.Log("WinGame");
        StartCoroutine(WinGameRoutine());
        
    }

    public IEnumerator WinGameRoutine (){
        GM.I.am.musicSource.Stop();
        currentLevel.player.StopInput();
        if(GM.I.cam.volumeStatusRout != null){
            StopCoroutine(GM.I.cam.volumeStatusRout);
        }
        while (GM.I.am.phrase != 0)
        {
            yield return null;
            GM.I.cam.statusVolumes[5].weight += Time.deltaTime * 0.5f;

        }

        while (GM.I.am.phrase != 16)
        {
            //currentLevel.transform.localScale *= 0.999f;
            GM.I.cam.statusVolumes[5].weight += Time.deltaTime * 1f;
            yield return null;
        }
        currentLevel.gameObject.SetActive(false);
        endgameMenu.SetActive(true);
        GM.I.am.musicSource.Stop();
        GM.I.am.musicSource.clip = GM.I.am.finalMusic;
        GM.I.am.musicSource.Play();
        GM.I.cam.statusVolumes[5].weight = 0f;
        gameObject.SetActive(false);
        
    }

    private void Update() {
        if(shot){
            shot = false;
            shotsTaken++;
        }
    }

    public void SpawnLevel(int id){
        gameObject.SetActive(true);
        shotsTaken = 0;
        dead = false;
        currentLevel = Instantiate(currentWorld.levelPrefabs[id]).GetComponent<Level>();
        currentLevel.Initialize(id);
    }

    private void LooseLevel()
    {
        StartCoroutine(Restart());
        
    }

    public IEnumerator Restart(){
        
        // Grace period, to sync with the beat
        GM.I.cam.SetVolumeStatus(3);

        currentLevel.player.StopInput();
        while (GM.I.am.beat != 4)
        {
            GM.I.am.musicSource.pitch = Mathf.Lerp(GM.I.am.musicSource.pitch, 0.5f, Time.deltaTime *lerpSpeed);
            yield return null; 
        }
        while (GM.I.am.beat != 0)
        {
            GM.I.am.musicSource.pitch = Mathf.Lerp(GM.I.am.musicSource.pitch, 0.5f, Time.deltaTime *lerpSpeed);
            yield return null; 
        }
        // Hide level
        transitionObject.position = new Vector3(xOffset, 0, 0);
        // Play ride sfx to climax at phrase = 32
        while (GM.I.am.beat != 2)
        {
            transitionObject.position = Vector3.Lerp(transitionObject.position, Vector3.zero, lerpSpeed * Time.deltaTime * 3f);
            GM.I.am.musicSource.pitch = Mathf.Lerp(GM.I.am.musicSource.pitch, 0.25f, Time.deltaTime *lerpSpeed);

            yield return null; 
        }
        transitionObject.position = Vector3.zero;
        currentLevel.player.gameObject.SetActive(false);

        
        // Setup win text, close level and open next level
        transitionText.text = "REBOOT";
        transitionText.color = Color.clear;
        logo.sprite = logoBad;
        logo.color = Color.clear;
        int id = currentLevel.id;
        Destroy(currentLevel.gameObject);
        SpawnLevel(id);

        while (GM.I.am.beat != 5)
        {
            GM.I.am.musicSource.pitch = Mathf.Lerp(GM.I.am.musicSource.pitch, 0.125f, Time.deltaTime *lerpSpeed);
            transitionText.color = Color.Lerp(transitionText.color , Color.red, lerpSpeed * 3f * Time.deltaTime);
            logo.color = Color.Lerp(transitionText.color , Color.red, lerpSpeed * 3f * Time.deltaTime);
            yield return null; 
        }
        GM.I.cam.SetVolumeStatus(0);

        // Reading Time !
        while (GM.I.am.beat != 4)
        {
            GM.I.am.musicSource.pitch = Mathf.Lerp(GM.I.am.musicSource.pitch, 0.5f, Time.deltaTime *lerpSpeed);
            GM.I.am.musicSource.volume = Mathf.Lerp(GM.I.am.musicSource.volume, 0.5f, Time.deltaTime *lerpSpeed/ 2f);
            
            if(GM.I.am.beat == 5){transitionText.text = "REBOOT.";}
            if(GM.I.am.beat == 6){transitionText.text = "REBOOT..";}
            if(GM.I.am.beat == 7){transitionText.text = "REBOOT...";}
            yield return null; 
        }



        // Text fade
        while (GM.I.am.beat != 5)
        {
            GM.I.am.musicSource.pitch = Mathf.Lerp(GM.I.am.musicSource.pitch, 1f, Time.deltaTime *lerpSpeed);
            GM.I.am.musicSource.volume = Mathf.Lerp(GM.I.am.musicSource.volume, 1f, Time.deltaTime *lerpSpeed/ 2f);
            transitionText.color = Color.Lerp(transitionText.color , Color.clear, lerpSpeed * 3f * Time.deltaTime);
            logo.color = Color.Lerp(transitionText.color , Color.clear, lerpSpeed * 3f * Time.deltaTime);
            yield return null; 
        }
        transitionText.color = Color.clear;
        logo.color = Color.clear;

        // Show new level
        while (GM.I.am.beat != 0)
        {
            GM.I.am.musicSource.pitch = 0;
            GM.I.am.musicSource.volume = Mathf.Lerp(GM.I.am.musicSource.volume, 1f, Time.deltaTime *lerpSpeed/ 2f);
            transitionObject.position = Vector3.Lerp(transitionObject.position, new Vector3(-xOffset, 0, 0), lerpSpeed * Time.deltaTime);
            yield return null; 
        }
        transitionObject.position = new Vector3(-xOffset, 0, 0);
        GM.I.am.musicSource.volume = 1f;
        GM.I.am.musicSource.pitch = 1f;

        GM.I.am.sfxSource.PlayOneShot(GM.I.gp.currentWorld.crash);
        GM.I.am.musicSource.Stop();
        GM.I.am.musicSource.Play();

        currentLevel.SpawnPlayer();
    }

    public void Split(Atom parent, List<Atom> children) {
        currentLevel.Split(parent, children);
        if(currentLevel.atoms.Count == 0){
            WinLevel();
        }
    }



    public void WinLevel(){
        Debug.Log("Win");
        StartCoroutine(Transition());
    }
    



    public IEnumerator Transition (){
        
        // Grace period, to sync with the beat
        GM.I.cam.SetVolumeStatus(2);
        GM.I.am.sfxSource.PlayOneShot(GM.I.gp.currentWorld.winLevel);

        currentLevel.player.StopInput();
        while (GM.I.am.phrase != 0)
        {
            yield return null; 
        }
        // Hide level
        transitionObject.position = new Vector3(xOffset, 0, 0);
        // Play ride sfx to climax at phrase = 32
        GM.I.am.sfxSource.PlayOneShot(GM.I.gp.currentWorld.rise);

        while (GM.I.am.phrase != 4)
        {
            transitionObject.position = Vector3.Lerp(transitionObject.position, Vector3.zero, lerpSpeed * Time.deltaTime);
            yield return null; 
        }
        transitionObject.position = Vector3.zero;
        currentLevel.player.gameObject.SetActive(false);

        
        // Setup win text, close level and open next level
        transitionText.text = "CORE STABILIZED";
        logo.sprite = logoBad;
        transitionText.color = Color.clear;
        logo.color = Color.clear;
        logo.sprite = logoGood;
        int id = currentLevel.id;
        int highscore = currentLevel.devHighscore;
        int score = GM.I.gp.shotsTaken;
        Destroy(currentLevel.gameObject);
        
        SpawnLevel(id + 1);

        while (GM.I.am.phrase != 16)
        {
            if(GM.I.am.phrase == 8){
                scoreText.text = "Shots : " + score;
            }
            transitionText.color = Color.Lerp(transitionText.color , Color.green, lerpSpeed * 3f * Time.deltaTime);
            logo.color = Color.Lerp(transitionText.color , Color.green, lerpSpeed * 3f * Time.deltaTime);
            yield return null; 
        }

        highscoreText.text = "Pierre's highscore : " + highscore;
        // Reading Time !
        while (GM.I.am.phrase != 24)
        {
            yield return null; 
        }
        GM.I.cam.SetVolumeStatus(0);
        scoreText.text = "";
        highscoreText.text = "";
        // Text fade
        while (GM.I.am.phrase != 30)
        {
            transitionText.color = Color.Lerp(transitionText.color , Color.clear, lerpSpeed * 3f * Time.deltaTime);
            logo.color = Color.Lerp(transitionText.color , Color.clear, lerpSpeed * 3f * Time.deltaTime);
            yield return null; 
        }
        logo.color = Color.clear;
        transitionText.color = Color.clear;

        // Show new level
        while (GM.I.am.phrase != 0)
        {
            transitionObject.position = Vector3.Lerp(transitionObject.position, new Vector3(-xOffset, 0, 0), lerpSpeed *  Time.deltaTime);
            yield return null; 
        }
        transitionObject.position = new Vector3(-xOffset, 0, 0);

        GM.I.am.sfxSource.PlayOneShot(GM.I.gp.currentWorld.crash);
        if(currentLevel.music != null){
            GM.I.am.musicSource.Stop();
            GM.I.am.musicSource.clip = currentLevel.music;
            GM.I.am.musicSource.Play();
        }

        currentLevel.SpawnPlayer();
    }

}
