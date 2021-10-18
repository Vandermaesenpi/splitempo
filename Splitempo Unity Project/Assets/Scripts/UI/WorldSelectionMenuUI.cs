using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelectionMenuUI : MenuUI
{
    public Text worldName;
    public Text worldTrophyText;
    public Text worldUnlockText;
    public Image worldIcon;
    public GameObject playButton;
    public GameObject previousLevelButton, nextLevelButton;
    public AnimationCurve animationCurve;
    public float xOffset, animSpeed;
    private WorldManager _worldManager;
    Coroutine selectWorldRoutine;
    [SerializeField] private CanvasGroup worldUI;

    public override void Initialize()
    {
        _worldManager = GM.I.world;
        selectWorldRoutine = StartCoroutine(SelectWorldRoutine(_worldManager.CurrentWorldIndex, 0));
        base.Initialize();
    }

    public void SelectWorld(int direction){
        if(selectWorldRoutine != null){
            StopCoroutine(selectWorldRoutine);
        }
        selectWorldRoutine = StartCoroutine(SelectWorldRoutine(_worldManager.CurrentWorldIndex + direction, direction));
    }


     IEnumerator SelectWorldRoutine(int worldIndex, int inputDirection)
    {
        HandleButtons(worldIndex);

        if (inputDirection != 0)
        {
            Vector3 iconPos = worldUI.transform.position;
            AudioManager.I.LerpMusicVolume(0, 1);
            for (float t = 0f; t < 1f; t += Time.deltaTime * animSpeed)
            {
                worldUI.transform.position = Vector3.Lerp(iconPos, iconPos + Vector3.right * xOffset * -inputDirection, animationCurve.Evaluate(t));
                worldUI.alpha = Mathf.Lerp(1f, 0f, animationCurve.Evaluate(t));
                yield return 0;
            }

            ShowWorldUI(worldIndex);

            for (float t = 0f; t < 1f; t += Time.deltaTime * animSpeed)
            {
                worldUI.transform.position = Vector3.Lerp(iconPos + Vector3.right * xOffset * inputDirection, iconPos, 1f - animationCurve.Evaluate(1f - t));
                worldUI.alpha = Mathf.Lerp(0f, 1f, animationCurve.Evaluate(t));
                yield return 0;
            }

            worldUI.transform.position = iconPos;

        }
        else
        {
            ShowWorldUI(0);
        }


    }

    private void HandleButtons(int i)
    {
        previousLevelButton.SetActive(i > 0);
        nextLevelButton.SetActive(i < _worldManager.WorldsCount - 1);
    }

    private void ShowWorldUI(int i)
    {
        World currentWorld = _worldManager.SetCurrentWorld(i);
        
        worldIcon.color = currentWorld.color;
        worldIcon.sprite = currentWorld.icon;

        bool worldIsLocked = _worldManager.currentWorld.IsLocked;

        if(worldIsLocked){
            worldName.text = "???????????";
            worldName.color = Color.grey;
        }else{
            worldName.text = currentWorld.displayName;
            worldName.color = currentWorld.color;
        }
        
        playButton.SetActive(!worldIsLocked);
        worldTrophyText.transform.parent.gameObject.SetActive(!worldIsLocked);
        worldUnlockText.transform.parent.parent.gameObject.SetActive(worldIsLocked);

        worldTrophyText.text = currentWorld.TrophiesInWorld + "/60";
        worldUnlockText.text = currentWorld.minimumTrophies + "";

        AudioManager.I.StartWorldMusic(currentWorld.menuMusic, currentWorld.bpm);
        
            
    }
}