using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Train;

namespace UI
{
    public class MouseCursorModifier : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private TrainCarMover _trainCar;
        [SerializeField] private GameObject _crosshairs;
        [SerializeField] private Camera _camera;

        // Start is called before the first frame update
        private void Start()
        {
            Cursor.visible = false;
            _crosshairs.SetActive(true);

            _trainCar.TrainCarCrashed += () =>
            {
                _crosshairs.SetActive(false);
                Cursor.visible = true;
            };
        }

        private void Update()
        {
            var newPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            _crosshairs.transform.position = newPos;
        }
    }
}
