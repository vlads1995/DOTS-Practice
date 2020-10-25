using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace PerfomanceTest
{
    public class PerfomanceTestMovableSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            
            Entities.ForEach((ref PerfomanceTestMovable movable, ref Translation translation) =>
            {
                var step = movable.dir * movable.speed * dt;
                translation.Value += step;
            
            }).Schedule();
        }
    }
}