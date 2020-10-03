using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Train;

namespace AI
{
    public class AiSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _aiPrefab;
        [SerializeField] private int[] _spawnWeight;
        [SerializeField] private Transform _centre;
        [SerializeField] private float _radius;
        [SerializeField] private TrainCarMover _trainCarMover;

        private void OnValidate()
        {
            if (_aiPrefab.Length != _spawnWeight.Length) _spawnWeight = new int[_aiPrefab.Length];
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
            goComp.Distance = _trainCarMover.TravelRadius;
        }
    }
}
