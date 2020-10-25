using Authoring;
using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Systems
{
    [AlwaysUpdateSystem]
    public class GameStateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var pelletQuery = GetEntityQuery(ComponentType.ReadOnly<Pellet>());
            var playerQuery = GetEntityQuery(ComponentType.ReadOnly<Player>());
            var enemyQuery = GetEntityQuery(ComponentType.ReadOnly<Enemy>());
            var spawnerQuery = GetEntityQuery(typeof(Spawner));
            
            var pelletCount = pelletQuery.CalculateEntityCount();
            
            if (playerQuery.CalculateEntityCount() > 0)
            {
                GameManager.instance.SetPelletCount(pelletCount);

                if (pelletCount <= 0)
                {
                    Entities
                        .WithAll<PhysicsVelocity>()
                        .ForEach((Entity e) =>
                        {
                            EntityManager.RemoveComponent<PowerPill>(e);
                            EntityManager.RemoveComponent<PhysicsVelocity>(e);
                            
                        }).WithStructuralChanges().Run();
                }
            }
            
            Entities
                .WithAll<Player, PhysicsVelocity>()
                .ForEach((Entity e, in Kill kill) =>
                {
                    EntityManager.RemoveComponent<PhysicsVelocity>(e);
                    EntityManager.RemoveComponent<Movable>(e);
                    GameManager.instance.LoseLife();

                    if (GameManager.instance.lives < 0)
                    {
                        var spawnerArray = spawnerQuery.ToEntityArray(Allocator.TempJob);

                        foreach (var spawner in spawnerArray)
                        {
                            EntityManager.DestroyEntity(spawner);
                        }

                        spawnerArray.Dispose();
                    }
                    
                    var enemyArray = enemyQuery.ToEntityArray(Allocator.TempJob);

                    foreach (var enemy in enemyArray)
                    {
                        EntityManager.AddComponentData(enemy, kill);
                        EntityManager.RemoveComponent<PhysicsVelocity>(enemy);
                        EntityManager.RemoveComponent<Movable>(enemy);
                        EntityManager.RemoveComponent<OnKill>(enemy);
                    }

                    enemyArray.Dispose();

                }).WithStructuralChanges().Run();
        }
    }
}