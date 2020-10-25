using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public class CameraSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var playerQuery = GetEntityQuery(typeof(Player), typeof(Movable), typeof(Translation));

            if (playerQuery.CalculateEntityCount() <= 0)
            {
                return;
            }

            var playerTranslation = GetComponent<Translation>(playerQuery.GetSingletonEntity());
            var minDist = float.MaxValue;

            var cameraQuery = GetEntityQuery(typeof(CameraTag), typeof(Follow));
            var cameraEntity = cameraQuery.GetSingletonEntity();
            var cameraFollow = GetComponent<Follow>(cameraEntity);

            Entities
                .WithAll<CameraPoint>()
                .ForEach((Entity e, in Translation translation) =>
                {
                    var currentDist = math.distance(translation.Value, playerTranslation.Value);
                    if (currentDist < minDist)
                    {
                        minDist = currentDist;
                        cameraFollow.target = e;
                        SetComponent(cameraEntity, cameraFollow);
                    }

                }).Run();
        }
    }
}