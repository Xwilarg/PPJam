using PPJam.Payer;
using PPJam.Player;
using UnityEngine;

namespace PPJam.Machine
{
    /// <summary>
    /// Allow for player to discard an object in his hands
    /// </summary>
    public class TrashMachine : MonoBehaviour, IInteractable
    {
        public GameObject GameObject => gameObject;

        public bool CanInteract(PlayerController pc) => pc.CarriedObject != null;

        public void Interact(PlayerController pc)
        {
            pc.EmptyHands();
        }
    }
}