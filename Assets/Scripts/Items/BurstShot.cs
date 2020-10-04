using Interface;
using UnityEngine;

namespace Items
{
    public class BurstShot : ItemMaster,IInteract
    {
        [SerializeField] private float _duration;
        public void Interact(int num)
        {
            ParentSpawn.BurstShot(_duration);
            Destroy(gameObject);
        }
    }
}
