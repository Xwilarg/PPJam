using PPJam.Payer;
using UnityEngine;

namespace PPJam.Player
{
    public interface IInteractable
    {
        public GameObject GameObject { get; }
        public bool CanInteract(PlayerController pc);
        public void Interact(PlayerController pc);
    }
}