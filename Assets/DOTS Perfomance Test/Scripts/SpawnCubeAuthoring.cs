using System.Collections.Generic;
using Components;
using Unity.Entities;
using UnityEngine;

namespace PerfomanceTest
{
    public class SpawnCubeAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public GameObject spawnObject;
        public int count;
    
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new SpawnCubeComponent()
            {
                spawnCube = conversionSystem.GetPrimaryEntity(spawnObject),
                spawnCount = count
            });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(spawnObject);
        }
    }
}