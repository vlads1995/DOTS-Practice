using Components;
using Unity.Entities;
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
            
            if (pelletQuery.CalculateEntityCount() <= 0)
            {
                GameManager.instance.Win();
            }

            if (playerQuery.CalculateEntityCount() <= 0)
            {
                GameManager.instance.Lose();
            }
        }
    }
}