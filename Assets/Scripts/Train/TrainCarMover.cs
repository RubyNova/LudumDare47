using System.Collections;
using System.Collections.Generic;
using Train;
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
    [Header("Jump Values")]
    [SerializeField]
    private int _enlargementIterations = 10;
    [SerializeField]
    private float _enlargementStep = 5f;
    [Header("Collision detection")]
    [SerializeField] private float _collisionRadius = 5f;

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
    
    private void FixedUpdate()
    {
        var foundCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), _collisionRadius);
        if (!foundCollider) return;
        var trackHealth = foundCollider.GetComponent<TrackHealth>();
        if (!trackHealth) return;
        if (trackHealth.TrackHealth1 <= 0 && !_isJumping) Debug.Log("You fuckin suck message play");
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
