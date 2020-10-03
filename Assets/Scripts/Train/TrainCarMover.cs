using System.Collections;
using System.Collections.Generic;
using Train;
using UnityEngine;
using System;

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
        private bool _isJumping;
        private bool _hasPlayedCrash;
        public float TravelRadius => _travelRadius;

        public event Action TrainCarCrashed;

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
            var foundCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), _collisionRadius);
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