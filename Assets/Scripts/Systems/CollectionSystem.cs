using Components;
using Unity.Entities;

namespace Systems
{
    public class CollectionSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var ecb = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
          
            Entities
                .WithAll<Player>()
                .ForEach((Entity playerEntity, DynamicBuffer<TriggerBuffer> tBuffer) =>
            {
                for (int i = 0; i < tBuffer.Length; i++)
                {
                    var e = tBuffer[i].Entity;
                    
                    if (HasComponent<Collectable>(e) && !HasComponent<Kill>(e))
                    {
                        ecb.AddComponent(e, new Kill() { timer = 0});
                        GameManager.instance.AddPoints(GetComponent<Collectable>(e).points);
                    }   
                    
                    if (HasComponent<PowerPill>(e) && !HasComponent<Kill>(e))
                    {
                        ecb.AddComponent(playerEntity, GetComponent<PowerPill>(e));
                        ecb.AddComponent(e, new Kill() { timer = 0});
                    }   
                }
                
            }).WithoutBurst().Run();
        }
    }
}