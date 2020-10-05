using Interface;
using UnityEngine;

namespace Items
{
    public class SpeedBoost : ItemMaster, IInteract
    {
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private float _duration;
        public void Interact(int num)
        {
            ParentSpawn.SpeedBoost(_speedMultiplier, _duration);
            AudioSource.PlayClipAtPoint(_itemActivationSound, transform.position);
            Destroy(gameObject);
        }
    }
}
