using PPJam.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PPJam.Alert
{
    public class Notification : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private TMP_Text _timerText;

        private float _timer;

        public void Init(EnergyInfo info)
        {
            _image.sprite = info.Icon;
            _timer = info.Timer;
            _timerText.text = $"{_timer / 60:00}:{_timer % 60:00}";
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            _timerText.text = $"{_timer / 60:00}:{_timer % 60:00}";

            if (_timer <= 0f)
            {
                Debug.LogWarning("Player lost!");
                Debug.Break();
            }
        }
    }
}