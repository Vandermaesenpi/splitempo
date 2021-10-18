using System.Collections;
using UnityEngine;
public class MenuUI : MonoBehaviour {
    
    private CanvasGroup _canvasGroup;
    
    Coroutine lerpRoutine;

    public virtual void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(){
        StartLerp(true);
        Initialize();
    }

    public virtual void Initialize(){}

    public void Hide(){
        StartLerp(false);
    }

    private void StartLerp(bool show){
        if(lerpRoutine != null){
            StopCoroutine(lerpRoutine);
        }
        lerpRoutine = StartCoroutine(LerpUI(show));
    }

    IEnumerator LerpUI(bool show){
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        for (float t = 0f; t < 1f; t+=Time.deltaTime * 5f)
        {
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, show ? t : 1f-t);
            yield return 0;
        }
        _canvasGroup.alpha = show ? 1 : 0;

        _canvasGroup.interactable = show;
        _canvasGroup.blocksRaycasts = show;
    }

}