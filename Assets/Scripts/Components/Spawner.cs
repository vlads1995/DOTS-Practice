using Unity.Entities;

namespace Components
{
    public struct Spawner : IComponentData
    {
        public Entity spawnPrefab;
        public Entity spawnObject;
    }
}