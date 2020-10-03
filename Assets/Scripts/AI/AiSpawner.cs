using System;
using System.Collections.Generic;
using Scoring;
using UnityEngine;
using Random = UnityEngine.Random;
using Train;

namespace AI
{
    public class AiSpawner : MonoBehaviour
    {
        [Header("Spawn Timer")]
        [SerializeField] private float __spawnRate;
        [SerializeField] private float _timerReduction;
        [SerializeField] private float _elapsedTimeToReduce;
        [Header("Dependencies")]
        [SerializeField] private GameObject[] _aiPrefab;
        [SerializeField] private int[] _spawnWeight;
        [SerializeField] private Transform _centre;
        [SerializeField] private float _radius;
        [SerializeField] private TrainCarMover _trainCarMover;
        [SerializeField] private HighScore _scoreboard;
        private float _spawnTimer;
        private float _timeElapsed;
        private float _previousTime = 0f;

        public List<GameObject> _aiSpawned = new List<GameObject>();

        private void OnValidate()
        {
            if (_aiPrefab.Length != _spawnWeight.Length) _spawnWeight = new int[_aiPrefab.Length];
        }

        private void Update()
        {
            _spawnTimer += Time.deltaTime;
            if (!(_spawnTimer > __spawnRate)) return;
            SpawnAi();
            _timeElapsed += _spawnTimer;
            if (_timeElapsed - _previousTime > _elapsedTimeToReduce)
            {
                _previousTime = _timeElapsed;
                __spawnRate -= _timerReduction;
            }
            _spawnTimer = 0f;
        }

        public void SpawnAi()
        {
            var spawnChance = Random.Range(0, 100);
            var spawn = false;
            GameObject aiToSpawn = null;
            for (int i = 0; i < _aiPrefab.Length; i++)
            {
                if (spawnChance < _spawnWeight[i]) aiToSpawn = _aiPrefab[i];
            }
            if (aiToSpawn == null) return;
            var direction = Random.Range(0, 360f) * Mathf.Deg2Rad;
            var spawnPos = _centre.position + (new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0f) * _radius);
            var go = Instantiate(aiToSpawn, spawnPos, Quaternion.identity);
            var goComp = go.GetComponent<AiMaster>();
            goComp._parentSpawn = this;
            goComp.Distance = _trainCarMover.TravelRadius;
        }

        public void RemoveAi(int num, GameObject go)
        {
            _scoreboard.CurrentScore += num;
            _aiSpawned.Remove(go);
            Destroy(go);
        }
    }
}
