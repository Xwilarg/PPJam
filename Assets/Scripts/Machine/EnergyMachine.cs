using PPJam.Manager;
using PPJam.Payer;
using PPJam.Player;
using PPJam.SO;
using UnityEngine;

namespace PPJam.Machine
{
    public class EnergyMachine : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private EnergyInfo _info;

        public EnergyInfo Info => _info;

        public GameObject GameObject => gameObject;

        public bool CanInteract(PlayerController pc) => true;

        public void Interact(PlayerController pc)
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            GameManager.Instance.Register(this);
        }
    }
}