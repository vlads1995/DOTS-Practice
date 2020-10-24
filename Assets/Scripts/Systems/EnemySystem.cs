using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace Systems
{
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class EnemySystem : SystemBase
    {
        private Unity.Mathematics.Random rng = new Unity.Mathematics.Random(1234);
        
        protected override void OnUpdate()
        {
            var raycaster = new MovementRaycast() { pw  = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld};
            rng.NextInt();
            var rngTemp = rng;
            
            Entities.ForEach((ref Movable movable, ref Enemy enemy, in Translation translation) =>
            {
                if (math.distance(translation.Value, enemy.previousCell) > 0.9f)
                {
                    enemy.previousCell = math.round(translation.Value);

                    //raycasts here
                    var validDir = new NativeList<float3>(Allocator.Temp);

                    if (!raycaster.CheckRay(translation.Value, new float3(0, 0, -1), movable.direction))
                    {
                        validDir.Add(new float3(0,0,-1));
                    }
                    
                    if (!raycaster.CheckRay(translation.Value, new float3(0, 0, 1), movable.direction))
                    {
                        validDir.Add(new float3(0,0,1));
                    }
                    
                    if (!raycaster.CheckRay(translation.Value, new float3(-1, 0, 0), movable.direction))
                    {
                        validDir.Add(new float3(-1,0,0));
                    }
                    
                    if (!raycaster.CheckRay(translation.Value, new float3(1, 0, 0), movable.direction))
                    {
                        validDir.Add(new float3(1,0,0));
                    }

                    movable.direction = validDir[rngTemp.NextInt(validDir.Length)];
                    
                    validDir.Dispose();

                }
            }).Schedule();
        }
        
        private struct MovementRaycast
        {
            [ReadOnly] public PhysicsWorld pw;
            
            public bool CheckRay(float3 pos, float3 direction, float3 currentDirection)
            {
                if (direction.Equals(-currentDirection)) return true;
                //if (direction.Equals(currentDirection)) return false;
                
                var ray = new RaycastInput()
                {
                    Start = pos,
                    End = pos + (direction * 0.9f),
                    Filter = new CollisionFilter()
                    {
                        GroupIndex = 0,
                        BelongsTo = 1u << 1,
                        CollidesWith = 1u << 2
                    }
                };

                return pw.CastRay(ray);
            }
        }
    }
}