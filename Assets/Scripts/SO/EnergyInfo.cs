using UnityEngine;

namespace PPJam.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/EnergyInfo", fileName = "EnergyInfo")]
    public class EnergyInfo : ScriptableObject
    {
        public string Name;
        public float Timer;
        public Sprite Icon;
    }
}