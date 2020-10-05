using UnityEngine;

namespace Items
{
    public class ItemMaster : MonoBehaviour
    {
        [SerializeField] protected AudioClip _itemActivationSound;
        public ItemSpawner ParentSpawn { get; set; }
    }
}
