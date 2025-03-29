using PPJam.Payer;
using PPJam.Player;
using System.Collections;
using TMPro;
using UnityEngine;

namespace PPJam.Machine
{
    public class TubeSendingMachine : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private TMP_Text _sendTimerText;

        [SerializeField]
        private EnergyMachine _associatedMachine;

        [SerializeField]
        private int _timerTime;

        [SerializeField]
        private GameObject _repairPartPrefab;

        public GameObject GameObject => gameObject;

        private SendingState _sendingState = SendingState.PendingUser;

        public bool CanInteract(PlayerController pc) =>
            (_sendingState == SendingState.PendingUser && pc.CarriedObject != null && pc.CarriedObject.Type == ObjectType.Drone) ||
            (_sendingState == SendingState.Received && pc.CarriedObject == null);

        public void Interact(PlayerController pc)
        {
            if (_sendingState == SendingState.PendingUser)
            {
                Destroy(pc.CarriedObject.GameObject);
                pc.CarriedObject = null;

                _sendingState = SendingState.Sending;
                StartCoroutine(PlayTimer());
            }
            else if (_sendingState == SendingState.Received)
            {
                _sendingState = SendingState.PendingUser;

                pc.CarriedObject = new()
                {
                    GameObject = Instantiate(_repairPartPrefab),
                    Type = ObjectType.Repair
                };
            }
            else throw new System.NotImplementedException();
        }

        private IEnumerator PlayTimer()
        {
            _sendTimerText.text = $"{_timerTime / 60:00}:{_timerTime % 60:00}";
            for (int i = 1; i <= _timerTime; i++)
            {
                yield return new WaitForSeconds(1f);
                var val = _timerTime - i;
                _sendTimerText.text = $"{val / 60:00}:{val % 60:00}";
            }

            _sendingState = SendingState.Received;
        }

        private void OnDrawGizmos()
        {
            if (_associatedMachine != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _associatedMachine.transform.position);
            }
        }

        private enum SendingState
        {
            PendingUser,
            Sending,
            Received
        }
    }
}