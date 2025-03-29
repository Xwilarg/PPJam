using PPJam.Alert;
using PPJam.SO;
using UnityEngine;

namespace PPJam.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _notificationContainer;

        [SerializeField]
        private GameObject _notificationPrefab;

        [SerializeField]
        private EnergyInfo[] _energies;

        private void Awake()
        {
            var go = Instantiate(_notificationPrefab, _notificationContainer);
            go.GetComponent<Notification>().Init(_energies[0]);
        }
    }
}
