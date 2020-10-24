using Components;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public class SpawnSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Spawner spawner, in Translation translation, in Rotation rot) =>
            {
                if (!EntityManager.Exists(spawner.spawnObject))
                {
                    spawner.spawnObject = EntityManager.Instantiate(spawner.spawnPrefab);
                    EntityManager.SetComponentData(spawner.spawnObject, translation);
                    EntityManager.SetComponentData(spawner.spawnObject, rot);
                }
                
            }).WithStructuralChanges().Run();
        }
    }
}