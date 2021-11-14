using UnityEngine;

public class Wall : MonoBehaviour, IInteractable
{
    public virtual void Interact(BallMovement interactor, RaycastHit hit)
    {
        interactor.Reflect(hit.normal);
    }
}