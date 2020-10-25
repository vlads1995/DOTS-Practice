using PerfomanceTest;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class CubeSpawnerSystem : SystemBase
{
    private bool isSetup;
    
    protected override void OnUpdate()
    {
        
        if (!isSetup)
        {
            Entities
                .ForEach((ref SpawnCubeComponent spawnData, in Translation translation, in Rotation rot) =>
                {
                    for (int i = 0; i < spawnData.spawnCount; i++)
                    {
                        EntityManager.Instantiate(spawnData.spawnCube);
                    }

                }).WithStructuralChanges().Run();

            Entities
                .ForEach((ref Entity e, ref PerfomanceTestMovable movable) =>
                {
                    SetComponent(e, new PerfomanceTestMovable()
                    {
                        speed = 0.3f,
                        dir = new float3(UnityEngine.Random.Range(-1f, 1f),UnityEngine.Random.Range(-1f, 1f),UnityEngine.Random.Range(-1f, 1f))
                    });

                }).WithoutBurst().Run();
            
            isSetup = true;
        }
    }
}
