using UnityEngine;
using System.Linq;
using Interface;

namespace Train
{
    public class BulletBehaviour : MonoBehaviour
    {
        [Header("Dependencies")] 
        [SerializeField] private float _radius;
        [SerializeField] private string _tagNameForEnemies;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Renderer _spriteRenderer;
        [SerializeField] private int _damage;
        
        // Update is called once per frame
        private void FixedUpdate()
        {
            var foundCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), _radius, -9);

            if (foundCollider == null) return;

            var interactable = foundCollider.GetComponent<IInteract>();
            if (interactable == null) return;
            interactable.Interact(_damage);
            Destroy(gameObject); //TODO: Add some kind of logic to damage the enemy. I assume Ian can handle that.
        }

        private void OnDrawGizmos() 
        {
           Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void Update()
        {
            transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
            
            if (!_spriteRenderer.isVisible)
            {
                Destroy(gameObject);
            }
        }
    }
}
