using Unity.Collections;
using Unity.Entities;

namespace Components
{
    public struct Health : IComponentData
    {
        public float value;
        public float invincibleTimer;
        public float killTimer;

        public NativeString64 damageSfx;
        public NativeString64 deathSfx;
    }
}