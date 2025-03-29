using PPJam.Payer;
using PPJam.Player;
using UnityEngine;

namespace PPJam.Machine
{
    public class SampleMachine : MonoBehaviour, IInteractable
    {
        public bool CanInteract => true;
        public GameObject GameObject => gameObject;

        public void Interact(PlayerController pc)
        {   
            throw new System.NotImplementedException();
        }
    }
}