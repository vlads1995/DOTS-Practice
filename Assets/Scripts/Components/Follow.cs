using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Follow : IComponentData
    {
        public Entity target;
        
        public float distance;
        public float speedMove;
        public float speedRotation;
        
        public float3 offset;
        public bool freezeXPos;
        public bool freezeYPos;
        public bool freezeZPos;
        public bool freezeRot;
    }
}