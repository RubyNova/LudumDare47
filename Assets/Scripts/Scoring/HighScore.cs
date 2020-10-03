using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scoring
{
    public class HighScore : MonoBehaviour
    {
        [SerializeField] private int _currentScore;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _highscoreText;

        public int CurrentScore
        {
            get => _currentScore;
            set => _currentScore = value;
        }

        private void Update()
        {
            if (_scoreText) _scoreText.text = "Score: " + _currentScore;
            if (_highscoreText) _highscoreText.text = "High Score: " + PlayerPrefs.GetFloat("score");
            if (!(PlayerPrefs.GetFloat("score", 0) <= _currentScore)) return;
            PlayerPrefs.SetFloat("score", _currentScore);
        }
    }
}
