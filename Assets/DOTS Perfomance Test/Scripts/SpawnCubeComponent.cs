using Unity.Entities;
using UnityEngine;

namespace PerfomanceTest
{
    public struct SpawnCubeComponent : IComponentData
    {
        public Entity spawnCube;
        public int spawnCount;
    }
}