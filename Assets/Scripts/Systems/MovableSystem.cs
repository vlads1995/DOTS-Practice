using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MovableSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float dt = Time.DeltaTime;
        
        Entities.ForEach((ref Movable movable, ref Translation translation, ref Rotation rot) =>
        {
            translation.Value += movable.speed * movable.direction * dt;
            rot.Value = math.mul(rot.Value.value, quaternion.RotateY(movable.speed * dt));
        }).Schedule();
    }
}
