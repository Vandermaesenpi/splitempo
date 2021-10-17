using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<PlayerBall> playerBalls;
    [SerializeField] private BallInputManager ballInputManager;
    public void Initialize()
    {
        ballInputManager.Initialize(this);
    }

    public void StopInput(){
        ballInputManager.StopInput();
    }

    public void UpdateBallsTargetRay(Vector3 _mousePos, Vector3 _currentMousePos)
    {
        foreach (PlayerBall ball in playerBalls)
        {
            ball.UpdateTargetRay(_mousePos, _currentMousePos);
        }
    }

    internal void HideBallsTargetRay()
    {
        foreach (PlayerBall ball in playerBalls)
        {
            ball.HideTargetRay();
        }    
    }

    internal void KickBalls(Vector3 _inputVector)
    {
        GM.I.gp.ShotTaken();
        foreach (PlayerBall ball in playerBalls)
        {
            ball.KickBall(_inputVector);
        }    
    }
}