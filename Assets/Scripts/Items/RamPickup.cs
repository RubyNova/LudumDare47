using Interface;
using Train;
using UnityEngine;

namespace Items
{
    public class RamPickup : ItemMaster, IInteract
    {
        [SerializeField] private float _ramDuration;
        public void Interact(int num)
        {
            ParentSpawn.ActivateRam(_ramDuration);
            AudioSource.PlayClipAtPoint(_itemActivationSound, transform.position);
            Destroy(gameObject);
        }
    }
}
