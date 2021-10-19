using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public LineRenderer directionLine;
    public TrailRenderer trail;
    public bool blue;

    [SerializeField] private BallMovement _ballMovement; 
    

    [Header("Audio")]
    [SerializeField] private List<AudioClip> sfxBounce;
    [SerializeField] private AudioClip sfxHurt;
    [SerializeField] private GameObject ballDeathVFX;



    public void UpdateTargetRay(Vector3 _mousePos, Vector3 _currentMousePos)
    {
        directionLine.SetPosition(0,  Vector3.zero);
        directionLine.SetPosition(1, -(_currentMousePos - _mousePos).normalized * Mathf.Min((_currentMousePos - _mousePos).magnitude, 2f));   
    }
    internal void HideTargetRay()
    {
        directionLine.SetPosition(0,  Vector3.zero);
        directionLine.SetPosition(1, Vector3.zero);   
    }

    internal void KickBall(Vector3 inputVector)
    {
        _ballMovement.KickBall(inputVector);
    }

    public void Bounce(){
        AudioManager.PlaySFX(sfxBounce[BeatManager.I.CurrentBeatInBar]);
    }

    public void Hurt(){
        AudioManager.PlaySFX(sfxHurt);
    }

    internal void DestroyVFX()
    {
        if(gameObject.activeInHierarchy){
            Disposable VFXObject = Instantiate(ballDeathVFX, transform.position, Quaternion.identity).GetComponent<Disposable>();
            VFXObject.Dispose();
            gameObject.SetActive(false);
        }
    }
}
