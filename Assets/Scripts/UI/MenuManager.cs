using System;
using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _menuObjects;
        [SerializeField] private bool[] _menuObjectState;

        private void OnValidate()
        {
            if (_menuObjectState.Length != _menuObjects.Length) _menuObjectState = new bool[_menuObjects.Length];
            for (int i = 0; i < _menuObjects.Length; i++)
            {
                _menuObjects[i].SetActive(_menuObjectState[i]);
            }
        }
    }
}
