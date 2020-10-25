using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class CameraAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public AudioListener audioListener;
        public Camera cam;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new CameraTag() { });
            conversionSystem.AddHybridComponent(audioListener);
            conversionSystem.AddHybridComponent(cam);
        }
    }
}