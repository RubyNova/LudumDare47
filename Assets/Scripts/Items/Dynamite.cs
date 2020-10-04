using Interface;

namespace Items
{
    public class Dynamite : ItemMaster, IInteract
    {
        public void Interact(int num)
        {
            for (int i = ParentSpawn.AiSpawner.AiSpawned.Count-1; i >= 0; i--)
            {
                var go = ParentSpawn.AiSpawner.AiSpawned[i];
                ParentSpawn.AiSpawner.AiSpawned.Remove(go);
                Destroy(go);
            }
            Destroy(gameObject);
        }
    }
}
