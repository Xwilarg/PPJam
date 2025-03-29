using PPJam.Payer;
using UnityEngine;

namespace PPJam.Player
{
    public interface IInteractable
    {
        public bool CanInteract { get; }
        public GameObject GameObject { get; }
        public void Interact(PlayerController pc);
    }
}