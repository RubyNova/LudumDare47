using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarMover : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform _circleCentre;
    [SerializeField] private float _travelRadius = 1f;
    [SerializeField] private float _movementSpeed = 50f;
    [SerializeField] private AudioSource _cartRailsSource;
    [SerializeField] private AudioSource _cartJumpSource;
    [SerializeField] private float _enlargementMultiplier;
    [SerializeField] private float _enlargementStep;

    private bool _isJumping;

    public float TravelRadius => _travelRadius;

    private void Start() 
    {
        _isJumping = false;
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

        //for (int i = 0; i < _enlargementIterations; i++)
        //{
        //    transform.localScale += (originalScale * Time.deltaTime * _enlargementStep);
        //    yield return null;
        //}

        //for (int i = 0; i < _enlargementIterations; i++)
        //{
        //    transform.localScale -= (originalScale * Time.deltaTime * _enlargementStep);
        //    yield return null;
        //}

        _cartJumpSource.Play();
        _cartRailsSource.UnPause();

        _isJumping = false;
    }
}
