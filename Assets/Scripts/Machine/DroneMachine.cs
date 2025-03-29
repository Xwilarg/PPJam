using PPJam.Payer;
using PPJam.Player;
using UnityEngine;

namespace PPJam.Machine
{
    public class DroneMachine : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GameObject _dronePrefab;

        public bool CanInteract(PlayerController pc) => pc.CarriedObject == null;
        public GameObject GameObject => gameObject;

        public void Interact(PlayerController pc)
        {
            pc.CarriedObject = new()
            {
                GameObject = Instantiate(_dronePrefab),
                Type = ObjectType.Drone
            };
        }
    }
}