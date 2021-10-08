using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    [Header("Canvases")]
    public GameObject startMenu;
    public GameObject levelSelectMenu;

    [Header("Start Menu")]
    public Text worldName;
    public Image worldIcon;
    public GameObject playButton;
    public GameObject levelSelectButton;

    [Header("World selection anim")]
    public float xOffset, animSpeed;
    public AnimationCurve animationCurve;


    [Header("Level Select Menu")]
    public Text worldNamLevelSelect;

    private void Start() {
        selectWorldRoutine = StartCoroutine(SelectWorldRoutine(GM.I.gp.worlds.IndexOf(GM.I.gp.currentWorld), 0));
    }

    public void SelectWorld(int direction){
        if(selectWorldRoutine != null){
            StopCoroutine(selectWorldRoutine);
        }

        selectWorldRoutine = StartCoroutine(SelectWorldRoutine(GM.I.gp.worlds.IndexOf(GM.I.gp.currentWorld)+ direction, direction));
    }

    Coroutine selectWorldRoutine;

    IEnumerator SelectWorldRoutine(int i, int dir){
        if(dir != 0){
            for (float t = 0f; t < 1f; t+=Time.deltaTime)
            {

                yield return 0;
            }
        }else{
            worldIcon.sprite = GM.I.gp.currentWorld.icon;
            worldName.text = GM.I.gp.currentWorld.displayName;
        }
    }
}
