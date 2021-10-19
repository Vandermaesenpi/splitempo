using UnityEngine;

public class Lava : MonoBehaviour, IInteractable
{
    public void Interact(BallMovement interactor, RaycastHit hit)
    {
        interactor.Reflect(hit.normal);
        GM.I.gp.Hurt();
        if(!GM.I.gp.dead)
            interactor._waitingForHurt = true;
    }
}