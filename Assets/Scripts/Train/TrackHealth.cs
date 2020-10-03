using UnityEngine;

namespace Train
{
    public class TrackHealth : MonoBehaviour
    {
        [SerializeField] private float _trackHealth;

        public float TrackHealth1
        {
            get => _trackHealth;
            set => _trackHealth = value;
        }
    }
}
