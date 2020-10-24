using Components;
using Unity.Entities;

namespace Systems
{
    public class DamageSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var dt = Time.DeltaTime;
            
            Entities.ForEach((DynamicBuffer<CollisionBuffer> col, ref Health hp) =>
            {
                for (int i = 0; i < col.Length; i++)
                {
                    if(hp.invincibleTimer <= 0 && HasComponent<Damage>(col[i].Entity))
                    {
                        hp.value -= GetComponent<Damage>(col[i].Entity).value;
                        hp.invincibleTimer = 1;
                    }
                }
                
            }).Schedule();

            Entities.WithNone<Kill>().ForEach((Entity e, ref Health hp) =>
            {
                hp.invincibleTimer -= dt;
                if (hp.value <= 0)
                {
                    EntityManager.AddComponentData(e, new Kill() {timer = hp.killTimer});
                }
                
            }).WithStructuralChanges().Run();

            var ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            var ecb = ecbSystem.CreateCommandBuffer().ToConcurrent();
            
            Entities.ForEach((Entity e, int entityInQueryIndex, ref Kill kill) =>
            {
                kill.timer -= dt;
                if (kill.timer <= 0)
                {
                    ecb.DestroyEntity(entityInQueryIndex, e);
                }
                
            }).Schedule();

            ecbSystem.AddJobHandleForProducer(this.Dependency);
        }
    }
}