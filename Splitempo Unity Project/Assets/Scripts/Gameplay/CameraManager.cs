using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : BeatListener
{
    [SerializeField] private PostProcessEffectManager _postProcessEffectManager;
    [SerializeField] private Shaker _camShake;
    public PostProcessVolume rythmVolume;
    public Camera gameCamera;

    private void Awake() {
        _postProcessEffectManager = GetComponent<PostProcessEffectManager>();
        _camShake = GetComponent<Shaker>();
    }

    public void CamShake(Vector3 direction){
        _camShake.Shake(direction);
    }   

    public void StartPostProcessingEffect(PostProcessEffectType _type){
        _postProcessEffectManager.SetVolumeStatus(_type);
    }

}
