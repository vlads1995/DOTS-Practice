using System.Collections.Generic;
using Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class OnKillAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public string sfxName;
        public GameObject spawnPrefab;
        public int pointValue;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new OnKill()
            {
                sfxName = new NativeString64(sfxName),
                pointValue = pointValue,
                spawnPrefab = conversionSystem.GetPrimaryEntity(spawnPrefab)
            });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(spawnPrefab);
        }
    }
}