using System.Collections;
using System.Collections.Generic;
using AI;
using Scoring;
using Train;
using UnityEngine;

namespace Items
{
    public class ItemSpawner : MonoBehaviour
    {
        [Header("Spawn Timer")]
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _timerReduction;
        [SerializeField] private float _elapsedTimeToReduce;
        [Header("Dependencies")]
        [SerializeField] private GameObject[] _itemPrefab;
        [SerializeField] private int[] _spawnWeight;
        [SerializeField] private Transform _centre;
        [SerializeField] private float _radius;
        [Header("For Items")] 
        [SerializeField] private TrackHealth[] _tracks;
        [SerializeField] private AiSpawner _aiSpawner;
        [SerializeField] private TrainCarMover _trainCarMover;
        [SerializeField] private TurretAimController _turret;
        private float _spawnTimer;
        private float _timeElapsed;
        private float _previousTime;
        private bool _gameOver;

        private float _fireRateTime;
        private float _fireTimeElapsed;
        private float _ramTime;
        private float _ramTimeElapsed;
        private float _burstTime;
        private float _burstTimeElapsed;

        private bool _fireTimeRunning;
        private bool _ramTimeRunning;
        private bool _burstTimeRunning;
        

        public TrackHealth[] Tracks
        {
            get => _tracks;
            set => _tracks = value;
        }

        public bool GameOver
        {
            get => _gameOver;
            set => _gameOver = value;
        }

        public AiSpawner AiSpawner => _aiSpawner;

        private void OnValidate()
        {
            if (_itemPrefab.Length != _spawnWeight.Length) _spawnWeight = new int[_itemPrefab.Length];
        }

        private void Update()
        {
            if (_gameOver) return;
            _spawnTimer += Time.deltaTime;
            if (!(_spawnTimer > _spawnRate)) return;
            SpawnItem();
            _timeElapsed += _spawnTimer;
            if (_timeElapsed - _previousTime > _elapsedTimeToReduce)
            {
                _previousTime = _timeElapsed;
                if (_spawnRate - _timerReduction > 0) _spawnRate -= _timerReduction;
            }

            _spawnTimer = 0f;
        }

        private void SpawnItem()
        {
            var spawnChance = Random.Range(0, 100);
            var spawn = false;
            GameObject itemToSpawn = null;
            for (int i = 0; i < _itemPrefab.Length; i++)
            {
                if (spawnChance < _spawnWeight[i]) itemToSpawn = _itemPrefab[i];
            }
            if (itemToSpawn == null) return;
            var direction = Random.Range(0, 360f) * Mathf.Deg2Rad;
            var spawnPos = _centre.position + (new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0f) * _radius);
            var go = Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
            var goComp = go.GetComponent<ItemMaster>();
            goComp.ParentSpawn = this;
        }
        #region Item Methods

        public void BurstShot(float time)
        {
            _turret.BurstShot = true;
            if (!_burstTimeRunning) StartCoroutine(BurstTime(time));
            else _burstTime += time;
            Debug.Log("Burst Time: " + _burstTime);
        }

        private IEnumerator BurstTime(float time)
        {
            _burstTimeElapsed = 0f;
            _burstTime = time;
            _burstTimeRunning = true;

            while (_burstTimeElapsed < _burstTime)
            {
                _burstTimeElapsed += Time.deltaTime;
                yield return null;
            }

            _burstTime = 0f;
            _burstTimeRunning = false;
            _turret.BurstShot = false;
        }

        public void IncreaseFireRate(float value, float time)
        {
            if (!_fireTimeRunning) StartCoroutine(FireRateTimer(value, time));
            else _fireRateTime += time;
            Debug.Log("Fire Rate Time: " + _fireRateTime);
        }

        private IEnumerator FireRateTimer(float value, float time)
        {
            _fireTimeElapsed = 0f;
            _fireRateTime = time;
            _fireTimeRunning = true;
            var ogFire = _turret.BulletCooldown;
            _turret.BulletCooldown /= value;

            while (_fireTimeElapsed < _fireRateTime)
            {
                _fireTimeElapsed += Time.deltaTime;
                yield return null;
            }

            _fireRateTime = 0f;
            _fireTimeRunning = false;
            _turret.BulletCooldown = ogFire;
        }

        public void ActivateRam(float time)
        {
            _trainCarMover.Ram.SetActive(true);
            if (!_ramTimeRunning) StartCoroutine(RamTime(time));
            else _ramTime += time;
            Debug.Log("Ram Time: " + _ramTime);
        }

        private IEnumerator RamTime(float time)
        {
            _ramTimeElapsed = 0f;
            _ramTime = time;
            _ramTimeRunning = true;

            while (_ramTimeElapsed < _ramTime)
            {
                _ramTimeElapsed += Time.deltaTime;
                yield return null;
            }

            _ramTime = 0f;
            _ramTimeRunning = false;
            _trainCarMover.Ram.SetActive(false);
        }

        public void SpeedBoost(float value, float time)
        {
            StartCoroutine(SpeedTimer(value, time));
        }

        private IEnumerator SpeedTimer(float value, float time)
        {
            var ogSpeed = _trainCarMover.MovementSpeed;
            _trainCarMover.MovementSpeed *= value;
            yield return new WaitForSeconds(time);
            _trainCarMover.MovementSpeed = ogSpeed;
            yield return null;
        }
        
        #endregion
    }
}
