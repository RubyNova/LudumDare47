using System;
using Interface;
using UnityEngine;

namespace Items
{
    public class AutoRepair : ItemMaster, IInteract
    {
        public void Interact(int num)
        {
            for (int i = 0; i < ParentSpawn.Tracks.Length; i++)
            {
                ParentSpawn.Tracks[i].TrackHealth1 = ParentSpawn.Tracks[i].StartHealth;
            }

            AudioSource.PlayClipAtPoint(_itemActivationSound, transform.position);
            
            Destroy(gameObject);
        }
    }
}
