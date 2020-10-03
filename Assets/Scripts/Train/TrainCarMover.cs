using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCarMover : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private Transform _circleCentre;
    [SerializeField]
    private float _travelRadius = 1f;
    [SerializeField]
    private float _movementSpeed = 50f;
    [SerializeField]
    private int _enlargementIterations = 10;
    [SerializeField]
    private float _enlargementStep = 5f;

    private bool _isJumping;

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
        var originalScale = transform.localScale;

        for (int i = 0; i < _enlargementIterations; i++)
        {
            transform.localScale += (originalScale * Time.deltaTime * _enlargementStep);
            yield return null;
        }

        for (int i = 0; i < _enlargementIterations; i++)
        {
            transform.localScale -= (originalScale * Time.deltaTime * _enlargementStep);
            yield return null;
        }

        _isJumping = false;
    }
}
