using Train;
using UnityEngine;

namespace AI
{
    public class MeleeAi : AiMaster
    {
        protected override void Moving()
        {
            var direction = Vector3.zero - transform.position;
            transform.position += direction * (Time.deltaTime * _movementSpeed);
            if (Vector3.Distance(Vector3.zero, transform.position) <= Distance) _currentState = AiState.Attacking;
        }

        protected override void Attacking()
        {
            if (!_trackHealth)
            {
                Debug.LogError(gameObject.name + ": Track Health type not found, unable to damage");
                return;
            }
            if (_trackHealth.TrackHealth1 < 0) Debug.Log("Track Destroyed");
            _attackTimer += Time.deltaTime;
            if (!(_attackTimer > _attackSpeed)) return;
            _trackHealth.TrackHealth1 -= _attackDamage;
            _attackTimer = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var trackHealth = other.GetComponent<TrackHealth>();
            if (!trackHealth) return;
            _trackHealth = trackHealth;
        }
    }
}
