using System;
using Train;
using UnityEditor.Animations;
using UnityEngine;

namespace AI
{
    public abstract class AiMaster : MonoBehaviour
    {
        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected float _attackSpeed;
        [SerializeField] protected float _attackDamage;
        [SerializeField] protected int _health;
        [SerializeField] protected int _pointValue;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected float _trackOffset;
        private readonly int _walkAnimation = Animator.StringToHash("Walking");
        private readonly int _attackAnimation = Animator.StringToHash("Attacking");
        protected float _attackTimer = 0;
        protected AiState _currentState = AiState.Spawned;
        protected TrackHealth _trackHealth;
        
        public AiSpawner ParentSpawn { get; set; }

        public float Distance { get; set; }

        public void Update()
        {
            switch (_currentState)
            {
                case AiState.Moving:
                    Moving();
                    _animator.SetBool(_walkAnimation, true);
                    _animator.SetBool(_attackAnimation, false);
                    break;
                case AiState.Attacking:
                    Attacking();
                    _animator.SetBool(_walkAnimation, false);
                    _animator.SetBool(_attackAnimation, true);
                    break;
                case AiState.Spawned:
                    if (Math.Abs(Distance) > 0) _currentState = AiState.Moving;
                    break;
                case AiState.Celebrating:
                    _animator.SetBool(_walkAnimation, false);
                    _animator.SetBool(_attackAnimation, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void Moving();

        protected abstract void Attacking();
    }
}
