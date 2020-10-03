using UnityEngine;

namespace Scoring
{
    public class HighScore : MonoBehaviour
    {
        [SerializeField] private int _currentScore;

        public int CurrentScore
        {
            get => _currentScore;
            set => _currentScore = value;
        }
    }
}
