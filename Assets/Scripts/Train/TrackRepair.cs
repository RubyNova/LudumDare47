using System;
using UnityEngine;

namespace Train
{
    public class TrackRepair : MonoBehaviour
    {
        [Header("Repair")] 
        [SerializeField] private GameObject _repairObject;

        [SerializeField] private KeyCode _repairKey;
        [SerializeField, Range(0f, 1f)] private float _timeSpeed;
        [SerializeField] private TurretAimController _turret;
        private bool _repairing;

        private void Awake()
        {
            if (_repairObject.activeSelf) _repairObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(_repairKey))
            {
                if (!_repairing)
                {
                    _repairObject.SetActive(true);
                    _turret.CanShoot = false;
                    Time.timeScale = _timeSpeed;
                    _repairing = true;
                }
                else
                {
                    _repairObject.SetActive(false);
                    _turret.CanShoot = true;
                    Time.timeScale = 1f;
                    _repairing = false;
                }
            }
        }
    }
}
