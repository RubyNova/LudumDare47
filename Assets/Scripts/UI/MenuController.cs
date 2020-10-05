using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Train;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        [Header("Optional dependencies")]
        [SerializeField] private TrainCarMover _carMover;
        [SerializeField] private GameObject _gameOverCanvas;

        private void Start()
        {
            if(_carMover == null) return;

            _carMover.TrainCarCrashed += () => {
                if (_gameOverCanvas == null) return;

                _gameOverCanvas.SetActive(true);
            };
        }

        public void LoadGame() => SceneManager.LoadScene(1);

        public void LoadMenu() => SceneManager.LoadScene(0);
    }
}