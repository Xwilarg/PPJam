using PPJam.Alert;
using PPJam.Machine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace PPJam.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private Transform _notificationContainer;

        [SerializeField]
        private GameObject _notificationPrefab;

        [SerializeField]
        private float _breakTimeMin, _breakTimeMax;

        private readonly List<EnergyMachine> _machines = new();

        private Dictionary<string, GameObject> _activeNotifications = new();

        private void Awake()
        {
            Instance = this;

            StartCoroutine(BreakRandomly());
        }

        public void Register(EnergyMachine machine)
        {
            _machines.Add(machine);
        }

        public void Repair(string key)
        {
            Assert.IsTrue(_activeNotifications.ContainsKey(key));

            Destroy(_activeNotifications[key]);
            _activeNotifications.Remove(key);
        }

        private IEnumerator BreakRandomly()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_breakTimeMin, _breakTimeMax));

                var availables = _machines.Where(x => !_activeNotifications.ContainsKey(x.Info.Name)); // Remove machines that are already broken

                if (!availables.Any()) continue;

                var go = Instantiate(_notificationPrefab, _notificationContainer);

                var targetMachine = availables.ElementAt(Random.Range(0, availables.Count()));
                go.GetComponent<Notification>().Init(targetMachine.Info);

                _activeNotifications.Add(targetMachine.Info.Name, go);
                targetMachine.Break();
            }
        }
    }
}
