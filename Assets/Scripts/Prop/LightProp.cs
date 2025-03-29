using UnityEngine;

namespace PPJam.Prop
{
    public class LightProp : MonoBehaviour, ISwitchable
    {
        private Light _light;

        public void Toggle(bool value)
        {
            _light.enabled = value;
        }

        private void Awake()
        {
            _light = GetComponent<Light>();
        }
    }
}