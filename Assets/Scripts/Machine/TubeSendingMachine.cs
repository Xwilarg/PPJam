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

        public GameObject GameObject => gameObject;

        private bool _isSending;

        public bool CanInteract(PlayerController pc) => !_isSending && pc.CarriedObject != null && pc.CarriedObject.Type == ObjectType.Drone;

        public void Interact(PlayerController pc)
        {
            Destroy(pc.CarriedObject.GameObject);
            pc.CarriedObject = null;

            _isSending = true;
            StartCoroutine(PlayTimer());
        }

        private IEnumerator PlayTimer()
        {
            _sendTimerText.text = $"{_timerTime / 60:00}:{_timerTime % 60:00}";
            for (int i = 0; i < _timerTime; i++)
            {
                yield return new WaitForSeconds(1f);
                var val = _timerTime - i;
                _sendTimerText.text = $"{val / 60:00}:{val % 60:00}";
            }
        }

        private void OnDrawGizmos()
        {
            if (_associatedMachine != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _associatedMachine.transform.position);
            }
        }
    }
}