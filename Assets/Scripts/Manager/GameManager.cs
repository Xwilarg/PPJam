using PPJam.Alert;
using PPJam.Machine;
using System.Collections.Generic;
using UnityEngine;

namespace PPJam.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private Transform _notificationContainer;

        [SerializeField]
        private GameObject _notificationPrefab;

        private List<EnergyMachine> _machines = new();

        private void Awake()
        {
            Instance = this;
        }

        public void Register(EnergyMachine machine)
        {
            _machines.Add(machine);
        }

        private void StartRandomAlert()
        {
            var go = Instantiate(_notificationPrefab, _notificationContainer);
            go.GetComponent<Notification>().Init(_machines[Random.Range(0, _machines.Count)].Info);
        }
    }
}
