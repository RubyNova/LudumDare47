using System;
using Interface;
using UnityEngine;

namespace Train
{
    public class Ram : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private int _damage;

        private void OnDrawGizmos() 
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
        private void FixedUpdate()
        {
            var foundCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), _radius, -9);

            if (foundCollider == null) return;

            var interactable = foundCollider.GetComponent<IInteract>();
            interactable?.Interact(_damage);
        }
    }
}
