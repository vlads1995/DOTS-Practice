using Components;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

public class CollisionSystem : SystemBase
{
    private struct CollisionSystemJob : ICollisionEventsJob
    {
        public BufferFromEntity<CollisionBuffer> collisions;
        
        public void Execute(CollisionEvent collisionEvent)
        {
            if (collisions.Exists(collisionEvent.EntityA))
            {
                collisions[collisionEvent.EntityA].Add(new CollisionBuffer() {Entity = collisionEvent.EntityB});
            }
            
            if (collisions.Exists(collisionEvent.EntityB))
            {
                collisions[collisionEvent.EntityB].Add(new CollisionBuffer() {Entity = collisionEvent.EntityA});
            }
        }
    }

    private struct TriggerSystemJob : ITriggerEventsJob
    {
        public BufferFromEntity<TriggerBuffer> triggers;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            if (triggers.Exists(triggerEvent.EntityA))
            {
                triggers[triggerEvent.EntityA].Add(new TriggerBuffer() {Entity = triggerEvent.EntityB});
            }
            
            if (triggers.Exists(triggerEvent.EntityB))
            {
                triggers[triggerEvent.EntityB].Add(new TriggerBuffer() {Entity = triggerEvent.EntityA});
            }
        }
    }
    
    protected override void OnUpdate()
    {
        var pw = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld;
        var sim = World.GetOrCreateSystem<StepPhysicsWorld>().Simulation;

        
        Entities.ForEach((DynamicBuffer<CollisionBuffer> collisions) =>
        {
            collisions.Clear();
        }).Run();

        var colJobHandle = new CollisionSystemJob()
        {
            collisions = GetBufferFromEntity<CollisionBuffer>()
        }.Schedule(sim, ref pw, this.Dependency);
        
        colJobHandle.Complete();
        
        
        
        Entities.ForEach((DynamicBuffer<TriggerBuffer> triggers) =>
        {
            triggers.Clear();
        }).Run();

        var triggerJobHandle = new TriggerSystemJob()
        {
            triggers = GetBufferFromEntity<TriggerBuffer>()
        }.Schedule(sim, ref pw, this.Dependency);
        
        triggerJobHandle.Complete();
    }
}