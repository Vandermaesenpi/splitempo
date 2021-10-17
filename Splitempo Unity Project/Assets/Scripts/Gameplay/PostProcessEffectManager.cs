using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessEffectManager : MonoBehaviour {
    [SerializeField] private List<PostProcessEffect> _effects;
    private PostProcessEffect _currentEffect;

    Coroutine _volumeStatusRoutine;

    public void SetVolumeStatus(PostProcessEffectType _type){
        if(_volumeStatusRoutine != null){
            StopCoroutine(_volumeStatusRoutine);
        }
        StopAllEffects();
        if(_type != PostProcessEffectType.None){
            _volumeStatusRoutine = StartCoroutine(StatusVolumeRoutine());
        }
    }
    private PostProcessEffect GetTargetedEffect(PostProcessEffectType _type){
        PostProcessEffect targetedEffect = null;
        foreach (PostProcessEffect effect in _effects){
            if(effect.Type == _type){
                targetedEffect = effect;
            }
        }    
        return targetedEffect;
    }

    private void StopAllEffects()
    {
        foreach (PostProcessEffect effect in _effects){
            effect.DisableEffect();
        }    
    }


    IEnumerator StatusVolumeRoutine(){
        _currentEffect.StartEffect();
        float t = 0;
        while (t < 1f)
        {
            _currentEffect.ProcessEffect(t);
            t += Time.deltaTime * BeatManager.I.CurrentBPMInSeconds * 2f;
            yield return 0;
        
        }
        _currentEffect.EndEffect();

    }

}