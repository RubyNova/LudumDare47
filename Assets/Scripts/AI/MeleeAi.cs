using Interface;
using Train;
using UnityEngine;

namespace AI
{
    public class MeleeAi : AiMaster, IInteract
    {
        [SerializeField] private float _colRadius;
        
        
        private void FixedUpdate()
        {
            if (_trackHealth) return;
            var foundCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), _colRadius, -8);
            if (!foundCollider) return;
            var trackHealth = foundCollider.GetComponent<TrackHealth>();
            if (!trackHealth) return;
            _trackHealth = trackHealth;
        }
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

            if (_trackHealth.TrackHealth1 <= 0) _currentState = AiState.Celebrating;
            _attackTimer += Time.deltaTime;
            if (!(_attackTimer > _attackSpeed)) return;
            _trackHealth.TrackHealth1 -= _attackDamage;
            _attackTimer = 0;
        }

        public void Interact(int num)
        {
            _health -= num;
            if (_health > 0) return;
            _parentSpawn.RemoveAi(_pointValue, gameObject);
        }
    }
}
