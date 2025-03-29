using PPJam.Manager;
using PPJam.Payer;
using PPJam.Player;
using PPJam.Prop;
using PPJam.SO;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField]
        private GameObject[] _attachedProps;
        private IEnumerable<ISwitchable> _props;

        private bool _isBroken;

        public EnergyInfo Info => _info;

        public GameObject GameObject => gameObject;

        public bool CanInteract(PlayerController pc) => _isBroken && pc.CarriedObject != null && pc.CarriedObject.RepairKey == _info.Name;

        public void Interact(PlayerController pc)
        {
            _isBroken = false;
            foreach (var p in _props) p.Toggle(true);
            GameManager.Instance.Repair(_info.Name);
            pc.EmptyHands();
        }

        private void Awake()
        {
            _props = _attachedProps.Select(x => x.GetComponent<ISwitchable>());
        }

        private void Start()
        {
            GameManager.Instance.Register(this);
        }

        public void Break()
        {
            _isBroken = true;
            foreach (var p in _props) p.Toggle(false);
        }
    }
}