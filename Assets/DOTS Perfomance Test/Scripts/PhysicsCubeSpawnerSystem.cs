using System.Transactions;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace PerfomanceTest
{
    public class PhysicsCubeSpawnerSystem : SystemBase
    {
        private bool isSetup;
    
        protected override void OnUpdate()
        {
            if (!isSetup)
            {
                Entities
                    .ForEach((in SpawnPhysicsCubeComponent spawnData) =>
                    {
                        var length = spawnData.length;
                        var width = spawnData.width;
                        var height = spawnData.height;
                        
                        for (int h = 0; h < height; h++)
                        {
                            for (int w = 0; w < width; w++)
                            {
                                for (int l = 0; l < length; l++)
                                {
                                    if (w != 0 && l != 0 && l != length - 1 && w != width - 1)
                                    {
                                        continue;
                                    }

                                    var ent = EntityManager.Instantiate(spawnData.physicsCube);
                                    EntityManager.SetComponentData(ent, new Translation() {Value = new float3((float)(l - length/2), (float)h + 0.5f, (float)(w - width/2))});;
                                }
                            }
                        }
                       
                    }).WithStructuralChanges().Run();

                isSetup = true;
            }
        }
    }
}