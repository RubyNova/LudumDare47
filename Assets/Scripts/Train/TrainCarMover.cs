using System.Collections;
using System.Collections.Generic;
using Scoring;
using Train;
using UnityEngine;
using System;
using AI;
using Items;

namespace Train
{
    public class TrainCarMover : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform _circleCentre;
        [SerializeField] private float _travelRadius = 1f;
        [SerializeField] private float _movementSpeed = 50f;
        [SerializeField] private AudioSource _cartRailsSource;
        [SerializeField] private AudioSource _cartJumpSource;
        [SerializeField] private AudioSource _crashSource;
        [SerializeField] private float _enlargementMultiplier;
        [SerializeField] private float _enlargementStep;
        [SerializeField] private float _collisionRadius;
        [SerializeField] private TurretAimController _aimController;
        [SerializeField] private CartRotator _cartRotator;
        [SerializeField] private float _pointTime;
        [SerializeField] private HighScore _scoring;
        [SerializeField] private GameObject _ram;

        [Header("For Game Over")] 
        [SerializeField] private AiSpawner _aiSpawner;
        [SerializeField] private ItemSpawner _itemSpawner;
        private float _pointTimer;
        private bool _isJumping;
        private bool _hasPlayedCrash;
        public float TravelRadius => _travelRadius;

        public event Action TrainCarCrashed;

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }

        public GameObject Ram
        {
            get => _ram;
            set => _ram = value;
        }

        private void Start()
        {
            _isJumping = false;
            _hasPlayedCrash = false;
        }

        // Update is called once per frame
        private void Update()
        {
            float angle = ((Time.time * _movementSpeed) % 360.0f) * Mathf.Deg2Rad;
            transform.position = _circleCentre.position + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * _travelRadius);

            if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            {
                _isJumping = true;
                StartCoroutine(HandleJumpAnim());
            }
        }

        private void FixedUpdate()
        {
            _pointTimer += Time.deltaTime;
            if (_pointTimer > _pointTime)
            {
                _scoring.CurrentScore += 1;
                _pointTimer = 0;
            }

            var foundCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y),
                _collisionRadius);
            if (!foundCollider) return;
            var trackHealth = foundCollider.GetComponent<TrackHealth>();
            if (!trackHealth) return;
            Debug.Log("Found track!");
            if (trackHealth.TrackHealth1 <= 0 && !_isJumping && !_hasPlayedCrash)
            {
                Debug.Log("You fuckin suck message play"); //TOPKEK
                _cartRailsSource.Stop();
                _crashSource.Play();
                _hasPlayedCrash = true;
                _aimController.enabled = false;
                _cartRotator.enabled = false;
                this.enabled = false;
                TrainCarCrashed?.Invoke();
                _aiSpawner.GameOver = true;
                _itemSpawner.GameOver = true;
            }
        }

        private IEnumerator HandleJumpAnim()
        {
            _cartRailsSource.Pause();
            var originalScale = transform.localScale;
            var targetScale = originalScale * _enlargementMultiplier;


            while (transform.localScale.magnitude < targetScale.magnitude)
            {
                transform.localScale += (originalScale * Time.deltaTime * _enlargementStep);
                yield return null;
            }

            transform.localScale = targetScale;

            while (transform.localScale.x > originalScale.x && transform.localScale.y > originalScale.y && transform.localScale.z > originalScale.z)
            {
                transform.localScale -= (originalScale * Time.deltaTime * _enlargementStep);
                yield return null;
            }

            transform.localScale = originalScale;

            _cartJumpSource.Play();
            _cartRailsSource.UnPause();

            _isJumping = false;
        }


    }
}