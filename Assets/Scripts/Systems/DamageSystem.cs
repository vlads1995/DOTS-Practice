using Components;
using Monobehaviour;
using Unity.Entities;
using Unity.Transforms;

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
                        AudioManager.instance.PlaySfxRequest(hp.damageSfx.ToString());
                    }
                }
                
            }).WithoutBurst().Run();

            Entities.WithNone<Kill>().ForEach((Entity e, ref Health hp) =>
            {
                hp.invincibleTimer -= dt;
                if (hp.value <= 0)
                {
                    AudioManager.instance.PlaySfxRequest(hp.deathSfx.ToString());
                    EntityManager.AddComponentData(e, new Kill() {timer = hp.killTimer});
                }
                
            }).WithStructuralChanges().Run();

            var ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            var ecb = ecbSystem.CreateCommandBuffer();
            
            Entities.ForEach((Entity e, ref Kill kill, in Translation translation, in Rotation rot) =>
            {   
                kill.timer -= dt;
                if (kill.timer <= 0)
                {
                    if (HasComponent<OnKill>(e))
                    {
                        var onKill = GetComponent<OnKill>(e);
                        AudioManager.instance.PlaySfxRequest(onKill.sfxName.ToString());
                        GameManager.instance.AddPoints(onKill.pointValue);

                        if (EntityManager.Exists(onKill.spawnPrefab))
                        {
                            var spawnedEntity = ecb.Instantiate(onKill.spawnPrefab);
                            ecb.AddComponent(spawnedEntity, translation);
                            ecb.AddComponent(spawnedEntity, rot);
                        }
                    }
                    
                    ecb.DestroyEntity(e);
                }
                
            }).WithoutBurst().Run();

            ecbSystem.AddJobHandleForProducer(this.Dependency);
        }
    }
}