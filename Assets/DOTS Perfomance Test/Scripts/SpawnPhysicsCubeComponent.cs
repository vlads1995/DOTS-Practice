using Unity.Entities;

namespace PerfomanceTest
{
    public class SpawnPhysicsCubeComponent : IComponentData
    {
        public Entity physicsCube;

        public int height = 100;
        public int width = 10;
        public int length = 10;
    }
}