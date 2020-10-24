using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Health : IComponentData
    {
        public float value;
        public float invincibleTimer;
        public float killTimer;
    }
}