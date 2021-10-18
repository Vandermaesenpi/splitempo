using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : MonoBehaviour
{
    #region singleton pattern
    private static CameraManager _instance;
    public static CameraManager I 
    {
        get {
            if (!_instance) {
                _instance = FindObjectOfType<CameraManager>();
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private PostProcessEffectManager _postProcessEffectManager;
    [SerializeField] private Shaker _camShake;
    public Camera gameCamera;

    public void CamShake(Vector3 direction){
        _camShake.Shake(direction);
    }   

    public void StartPostProcessingEffect(PostProcessEffectType _type){
        _postProcessEffectManager.SetVolumeStatus(_type);
    }

}
