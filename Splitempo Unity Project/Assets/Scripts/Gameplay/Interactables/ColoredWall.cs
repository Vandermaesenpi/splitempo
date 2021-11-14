using UnityEngine;

public class ColoredWall : Wall
{
    [SerializeField] bool blue;
    public override void Interact(BallMovement interactor, RaycastHit hit)
    {
        if(interactor.MyPlayerBall.blue != blue){
            base.Interact(interactor, hit);
        }
    }
}