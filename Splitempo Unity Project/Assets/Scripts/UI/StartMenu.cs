using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    WorldManager _worldManager;

    [Header("Canvases")]
    public GameObject startMenu;
    public GameObject levelSelectMenu;

    [Header("Start Menu")]
    public CanvasGroup worldUI;
    public Text worldName;
    public Text worldTrophyText;
    public Text worldUnlockText;
    public Image worldIcon;
    public GameObject playButton;
    public GameObject previousLevelButton, nextLevelButton;

    [Header("World selection anim")]
    public AnimationCurve animationCurve;
    public float xOffset, animSpeed;


    [Header("Level Select Menu")]
    public Text worldNamLevelSelect;

    private void Start() {
        selectWorldRoutine = StartCoroutine(SelectWorldRoutine(_worldManager.CurrentWorldIndex, 0));
    }

    public void SelectWorld(int direction){
        if(selectWorldRoutine != null){
            StopCoroutine(selectWorldRoutine);
        }

        selectWorldRoutine = StartCoroutine(SelectWorldRoutine(_worldManager.CurrentWorldIndex + direction, direction));
    }

    Coroutine selectWorldRoutine;

    IEnumerator SelectWorldRoutine(int i, int dir){
        
        previousLevelButton.SetActive( i > 0 );
        nextLevelButton.SetActive( i < _worldManager.WorldsCount - 1 );

        if(dir != 0){
            Vector3 iconPos = worldUI.transform.position;
            for (float t = 0f; t < 1f; t+=Time.deltaTime * animSpeed)
            {
                worldUI.transform.position = Vector3.Lerp(iconPos, iconPos + Vector3.right * xOffset * -dir, animationCurve.Evaluate(t));
                worldUI.alpha = Mathf.Lerp(1f, 0f, animationCurve.Evaluate(t));
                yield return 0;
            }

            InitializeWorldUI(i);

            for (float t = 0f; t < 1f; t+=Time.deltaTime * animSpeed)
            {
                worldUI.transform.position = Vector3.Lerp(iconPos + Vector3.right * xOffset * dir, iconPos, 1f - animationCurve.Evaluate(1f - t));
                worldUI.alpha = Mathf.Lerp(0f, 1f, animationCurve.Evaluate(t));
                yield return 0;
            }

            worldUI.transform.position = iconPos;

        }else
        {
            InitializeWorldUI(0);
        }


    }

    private void InitializeWorldUI(int i)
    {
        World currentWorld = _worldManager.SetCurrentWorld(i);
        
        bool worldIsLocked = _worldManager.currentWorld.IsLocked;

        if(worldIsLocked){
            worldIcon.color = currentWorld.color;
            worldIcon.sprite = currentWorld.icon;
            worldName.text = "???????????";
            worldName.color = Color.grey;
        }else{
            worldIcon.color = currentWorld.color;
            worldIcon.sprite = currentWorld.icon;
            worldName.text = currentWorld.displayName;
            worldName.color = currentWorld.color;
        }
        
        playButton.SetActive(!worldIsLocked);
        worldTrophyText.transform.parent.gameObject.SetActive(!worldIsLocked);
        worldUnlockText.transform.parent.parent.gameObject.SetActive(worldIsLocked);

        worldTrophyText.text = currentWorld.TrophiesInWorld + "/60";
        worldUnlockText.text = currentWorld.minimumTrophies + "";
        
            
    }

}
