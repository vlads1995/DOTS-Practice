using Components;
using Monobehaviour;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var dt = Time.DeltaTime;
        
        Entities.ForEach((ref Movable movable, in Player player) =>
        {
            movable.direction = new float3(x, 0, y);
        }).Schedule();

        var ecb = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();

        Entities
            .WithAll<Player>()
            .ForEach((Entity e, ref Health hp, ref PowerPill pill, ref Damage dmg) =>
            {
                dmg.value = 100;
                pill.pillTimer -= dt;
                hp.invincibleTimer = pill.pillTimer;
                AudioManager.instance.PlayMusicRequest("powerup");
                
                if (pill.pillTimer <= 0)
                {
                    AudioManager.instance.PlayMusicRequest("game");
                    dmg.value = 0;
                    ecb.RemoveComponent<PowerPill>(e);
                }

            }).WithoutBurst().Run();
    }
}