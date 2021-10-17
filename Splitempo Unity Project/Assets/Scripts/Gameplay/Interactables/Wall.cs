using UnityEngine;

public class Wall : IInteractable
{
    public void Interact(BallMovement interactor, RaycastHit hit)
    {
        interactor.Reflect(hit.normal);
    }
}