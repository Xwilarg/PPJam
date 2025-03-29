using PPJam.Payer;
using PPJam.Player;
using TMPro;
using UnityEngine;

namespace PPJam.Machine
{
    public class TubeSendingMachine : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private TMP_Text _sendTimerText;

        public GameObject GameObject => gameObject;

        public bool CanInteract(PlayerController pc) => pc.CarriedObject != null && pc.CarriedObject.Type == ObjectType.Drone;

        public void Interact(PlayerController pc)
        {
            Destroy(pc.CarriedObject.GameObject);
            pc.CarriedObject = null;
        }
    }
}