using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace Monobehaviour
{
    public class PlayerAnim : MonoBehaviour
    {
        public Animator playerAnim;
        public Transform playerTr;

        private void Update()
        {
            var em = World.DefaultGameObjectInjectionWorld.EntityManager;
            var playerQuery = em.CreateEntityQuery(typeof(Player), typeof(PhysicsVelocity), typeof(Translation), typeof(Rotation), typeof(Movable));

            if (playerQuery.CalculateEntityCount() > 0)
            {
                ResetAnim();
                
                var playerEntity = playerQuery.GetSingletonEntity();
                playerTr.position = em.GetComponentData<Translation>(playerEntity).Value;

                var speed = math.length(em.GetComponentData<PhysicsVelocity>(playerEntity).Linear);
                var dir = em.GetComponentData<Movable>(playerEntity).direction;

                if (math.length(dir) > 0.2f)
                {
                    playerTr.rotation = Quaternion.LookRotation(dir);   
                }
                
                playerAnim.SetFloat("Speed", speed * 10f);
            }
        }

        public void Win()
        {
            playerAnim.SetBool("Win", true);
            playerAnim.SetFloat("Speed", 0);
        }

        public void Lose()
        {
            playerAnim.SetBool("Hit", true);
            playerAnim.SetFloat("Speed", 0);
        }

        public void ResetAnim()
        {
            playerAnim.SetBool("Win", false);
            playerAnim.SetBool("Hit", false);
        }
    }
}