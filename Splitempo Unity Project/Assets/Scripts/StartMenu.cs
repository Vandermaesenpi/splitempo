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
    public Text worldTrophyText;
    public Image worldIcon;
    public GameObject playButton;
    public GameObject levelSelectButton;
    public GameObject previousLevelButton, nextLevelButton;

    [Header("World selection anim")]
    public AnimationCurve animationCurve;
    public float xOffset, animSpeed;


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
        
        previousLevelButton.SetActive(GM.I.gp.worlds.IndexOf(GM.I.gp.currentWorld)+dir> 0);
        nextLevelButton.SetActive(GM.I.gp.worlds.IndexOf(GM.I.gp.currentWorld)+dir< GM.I.gp.worlds.Count - 1);

        if(dir != 0){
            Vector3 iconPos = worldIcon.transform.position;
            for (float t = 0f; t < 1f; t+=Time.deltaTime * animSpeed)
            {
                worldIcon.transform.position = Vector3.Lerp(iconPos, iconPos + Vector3.right * xOffset * -dir, animationCurve.Evaluate(t));
                worldIcon.color = Color.Lerp(GM.I.gp.currentWorld.color, Color.clear, animationCurve.Evaluate(t));
                worldName.color = Color.Lerp(GM.I.gp.currentWorld.color, Color.clear, animationCurve.Evaluate(t));
                yield return 0;
            }
            GM.I.gp.currentWorld = GM.I.gp.worlds[GM.I.gp.worlds.IndexOf(GM.I.gp.currentWorld)+ dir];
            worldIcon.transform.position = iconPos + Vector3.right * xOffset * dir;
            worldIcon.color = Color.clear;
            worldName.color = Color.clear;
            worldIcon.sprite = GM.I.gp.currentWorld.icon;
            worldName.text = GM.I.gp.currentWorld.displayName;

            for (float t = 0f; t < 1f; t+=Time.deltaTime * animSpeed)
            {
                worldIcon.transform.position = Vector3.Lerp(iconPos + Vector3.right * xOffset * dir, iconPos,  animationCurve.Evaluate(t));
                worldIcon.color = Color.Lerp(Color.clear, GM.I.gp.currentWorld.color, animationCurve.Evaluate(t));
                worldName.color = Color.Lerp(Color.clear, GM.I.gp.currentWorld.color, animationCurve.Evaluate(t));
                yield return 0;
            }

            worldIcon.transform.position = iconPos;
            worldIcon.color = GM.I.gp.currentWorld.color;
            worldName.color = GM.I.gp.currentWorld.color;

        }else{
            GM.I.gp.currentWorld = GM.I.gp.worlds[GM.I.gp.worlds.IndexOf(GM.I.gp.currentWorld)+ dir];
            worldIcon.sprite = GM.I.gp.currentWorld.icon;
            worldIcon.color = GM.I.gp.currentWorld.color;
            worldName.text = GM.I.gp.currentWorld.displayName;
            worldIcon.color = GM.I.gp.currentWorld.color;
        }

        
    }
}
