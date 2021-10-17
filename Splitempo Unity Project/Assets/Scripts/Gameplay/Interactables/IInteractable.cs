using UnityEngine;

public interface IInteractable
{
    void Interact(BallMovement interactor, RaycastHit hit);
}