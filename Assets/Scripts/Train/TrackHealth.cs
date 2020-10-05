using Scoring;
using UnityEngine;
using TMPro;

namespace Train
{
    public class TrackHealth : MonoBehaviour
    {
        [SerializeField] private float _trackHealth;
        [SerializeField] private TMP_Text _repairCost;
        [SerializeField] private HighScore _score;
        private float _startHealth;

        public float StartHealth => _startHealth;

        public float TrackHealth1
        {
            get => _trackHealth;
            set => _trackHealth = value;
        }

        private void Awake()
        {
            _startHealth = _trackHealth;
        }

        private void Update()
        {
            _repairCost.text = (_startHealth - _trackHealth).ToString();
        }

        public void Repair()
        {
            var amount = _startHealth - _trackHealth;
            if (_score.CurrentScore >= amount)
            {
                _trackHealth += amount;
                _score.CurrentScore -= (int) amount;
            }
            else
            {
                amount = _score.CurrentScore;
                _trackHealth += amount;
                _score.CurrentScore -= (int) amount;
            }
        }
    }
}
