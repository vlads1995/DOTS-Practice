using Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class HealthAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float hpValue;
        public float killTimer;

        public string dmgSfx;
        public string dthSfx;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new Health()
            {
                invincibleTimer = 0,
                value = hpValue,
                killTimer = killTimer,
                damageSfx = new NativeString64(dmgSfx),
                deathSfx = new NativeString64(dthSfx)
            });
        }
    }
}