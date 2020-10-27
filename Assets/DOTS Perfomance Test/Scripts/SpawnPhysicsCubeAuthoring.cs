using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace PerfomanceTest
{
    public class SpawnPhysicsCubeAuthoring: MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public GameObject physicsCube;

        public int height = 100;
        public int width = 10;
        public int length = 10;
    
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new SpawnPhysicsCubeComponent()
            {
                physicsCube = conversionSystem.GetPrimaryEntity(physicsCube),
                height = height,
                width = width,
                length = length
            });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(physicsCube);
        }
    }
}