﻿using System;
using Train;
using UnityEngine;

namespace AI
{
    public abstract class AiMaster : MonoBehaviour
    {
        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected float _attackSpeed;
        [SerializeField] protected float _attackDamage;
        protected float _attackTimer = 0;
        protected AiState _currentState = AiState.Spawned;
        protected TrackHealth _trackHealth;
        
        public float Distance { get; set; }

        public void Update()
        {
            switch (_currentState)
            {
                case AiState.Moving:
                    Moving();
                    break;
                case AiState.Attacking:
                    Attacking();
                    break;
                case AiState.Spawned:
                    if (Math.Abs(Distance) > 0) _currentState = AiState.Moving;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void Moving();

        protected abstract void Attacking();
    }
}