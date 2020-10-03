using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Train
{
    public class CartRotator : MonoBehaviour
    {
        [SerializeField] private Transform _positionToLookAt;
        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            var dir = (transform.position - _positionToLookAt.position).normalized;
            float distance = Vector3.Distance(transform.position, _positionToLookAt.position);
            var newRightPos = _positionToLookAt.position + (dir * (distance * 2));
            transform.right = newRightPos;
        }
    }
}
