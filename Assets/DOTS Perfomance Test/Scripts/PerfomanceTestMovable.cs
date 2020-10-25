using Unity.Entities;
using Unity.Mathematics;

namespace PerfomanceTest
{
    [GenerateAuthoringComponent]
    public struct PerfomanceTestMovable : IComponentData
    {
        public float3 dir;
        public float speed;
    }
}