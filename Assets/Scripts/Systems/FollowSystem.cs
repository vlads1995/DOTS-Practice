using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class FollowSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            
            Entities
                .WithAll<Translation, Rotation>()
                .ForEach((Entity e, in Follow follow) =>
            {  
                
                if(!EntityManager.Exists(follow.target))
                {
                    return;
                }
                
                var currentPos = GetComponent<Translation>(e).Value;
                var currentRot = GetComponent<Rotation>(e).Value;
                
                var targetPos = GetComponent<Translation>(follow.target).Value;
                var targetRot = GetComponent<Rotation>(follow.target).Value;

                targetPos += math.mul(targetRot, targetPos) * -follow.distance;
                targetPos += follow.offset;

                targetPos.x = follow.freezeXPos ? currentPos.x : targetPos.x;
                targetPos.y = follow.freezeYPos ? currentPos.y : targetPos.y;
                targetPos.z = follow.freezeZPos ? currentPos.z : targetPos.z;
                targetRot = follow.freezeRot ? currentRot : targetRot;

                targetPos = math.lerp(currentPos, targetPos, dt * follow.speedMove);
                targetRot = math.lerp(currentRot.value, targetRot.value, dt * follow.speedRotation);
                
                SetComponent(e, new Translation() { Value = targetPos});
                SetComponent(e, new Rotation() { Value = targetRot});

                
            }).WithoutBurst().Run();
        }
    }
}