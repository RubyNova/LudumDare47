using Interface;
using UnityEngine;

namespace Items
{
    public class FireRate : ItemMaster, IInteract
    {
        [SerializeField] private float _fireRateMultiplier;
        [SerializeField] private float _duration;
        public void Interact(int num)
        {
            ParentSpawn.IncreaseFireRate(_fireRateMultiplier, _duration);
            Destroy(gameObject);
        }
    }
}
