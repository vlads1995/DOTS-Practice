using Unity.Entities;
using Unity.Collections;

namespace Components
{
    public struct OnKill : IComponentData
    {
        public NativeString64 sfxName;
        public Entity spawnPrefab;
        public int pointValue;
    }
}