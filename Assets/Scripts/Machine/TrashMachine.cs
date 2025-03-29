using PPJam.Payer;
using PPJam.Player;
using UnityEngine;

namespace PPJam.Machine
{
    public class TrashMachine : MonoBehaviour, IInteractable
    {
        public GameObject GameObject => gameObject;

        public bool CanInteract(PlayerController pc) => pc.CarriedObject != null;

        public void Interact(PlayerController pc)
        {
            Destroy(pc.CarriedObject.GameObject);
            pc.CarriedObject = null;
        }
    }
}