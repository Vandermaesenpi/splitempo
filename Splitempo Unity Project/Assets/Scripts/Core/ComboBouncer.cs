using System.Collections;
using UnityEngine;

public class ComboBouncer : MonoBehaviour
{

    [SerializeField] private Color downColor;
    [SerializeField] private Color upColor;
    
    [SerializeField] private AnimationCurve bounceCurve;

    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    Coroutine _bounceRoutine;

    private void Awake() {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void Bounce(){
        if(_bounceRoutine != null){
            StopCoroutine(_bounceRoutine);
        }
        _bounceRoutine = StartCoroutine(BounceRoutine());
    }

    IEnumerator BounceRoutine(){
        SetColor(upColor);
        for (float i = 0; i < BeatManager.I.CurrentBPMInSeconds; i+= Time.deltaTime)
        {
            SetColor(Color.Lerp(upColor, downColor, i/BeatManager.I.CurrentBPMInSeconds));
            yield return 0;
        }
        SetColor(downColor);
    }

    private void SetColor(Color _color){
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor("_Color", _color);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}