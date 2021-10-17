using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessEffect : MonoBehaviour
{

    [SerializeField] private PostProcessEffectType _effectType;
    public PostProcessEffectType Type => _effectType;
    [SerializeField] private AnimationCurve _animationCurve;
    private PostProcessVolume _volume;
    private void Awake() {
        _volume = GetComponent<PostProcessVolume>();   
    }

    public void DisableEffect(){
        _volume.enabled = false;
    }
    public void StartEffect(){
        _volume.enabled = true;
        _volume.weight = Mathf.Lerp(0, 1, _animationCurve.Evaluate(0));
    }

    public void ProcessEffect(float _time){
        _volume.weight = Mathf.Lerp(0, 1, _animationCurve.Evaluate(_time));
    }

    public void EndEffect(){
        _volume.weight = Mathf.Lerp(0, 1, _animationCurve.Evaluate(1));
    }

}

public enum PostProcessEffectType{
    Hurt,
    Win,
    Loose,
    BossHurt,
    BossWin,
    None
}