using PPJam.Manager;
using PPJam.Payer;
using PPJam.Player;
using PPJam.SO;
using UnityEngine;

namespace PPJam.Machine
{
    /// <summary>
    /// Game objectives, sometimes break and need to be repaired
    /// </summary>
    public class EnergyMachine : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private EnergyInfo _info;

        private bool _isBroken;

        public EnergyInfo Info => _info;

        public GameObject GameObject => gameObject;

        public bool CanInteract(PlayerController pc) => _isBroken && pc.CarriedObject != null && pc.CarriedObject.RepairKey == _info.Name;

        public void Interact(PlayerController pc)
        {
            _isBroken = false;
            GameManager.Instance.Repair(_info.Name);
            pc.EmptyHands();
        }

        private void Start()
        {
            GameManager.Instance.Register(this);
        }

        public void Break()
        {
            _isBroken = true;
        }
    }
}