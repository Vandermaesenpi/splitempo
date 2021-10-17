using UnityEngine;

public class Lava : IInteractable
{
    public void Interact(BallMovement interactor, RaycastHit hit)
    {
        interactor.Reflect(hit.normal);
        GM.I.gp.Hurt();
    }
}