using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<PlayerBall> playerBalls;
    public void Initialize()
    {
        foreach (PlayerBall ball in playerBalls)
        {
            ball.Initialize();
        }
    }

    public void StopInput(){
        foreach (PlayerBall ball in playerBalls)
        {
            ball.StopInput();
        }
    }

}