using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    [UpdateAfter(typeof(InputHandlerSystem))]
    [CreateAfter(typeof(InputHandlerSystem))]
    public partial struct MovementSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputComponent>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var inputEntity = SystemAPI.GetSingletonEntity<InputComponent>();
            var inputcomp = SystemAPI.GetComponent<InputComponent>(inputEntity);
            
            foreach (var (movementComp,transformComp) in SystemAPI.Query<RefRW<MovementComponent>,RefRW<LocalTransform>>().WithAll<ControlEntityTag>())
            {

                var speedMultiplier = Time.deltaTime * movementComp.ValueRO.speed;
                
                speedMultiplier = Time.deltaTime * movementComp.ValueRO.speed;
                
                if (movementComp.ValueRO.vector.y > 0)
                {
                    transformComp.ValueRW.Position += new float3(0, 1 * speedMultiplier, 0);
                }
                else if (movementComp.ValueRO.vector.y < 0)
                {
                    transformComp.ValueRW.Position -= new float3(0, 1 * speedMultiplier, 0);
                }
                else
                {
                    transformComp.ValueRW.Position -= new float3(0, 0, 0);
                }

                if (movementComp.ValueRO.vector.x > 0)
                {
                    transformComp.ValueRW.Position += new float3(1 * speedMultiplier, 0, 0);
                }
                else if (movementComp.ValueRO.vector.x < 0)
                {
                    transformComp.ValueRW.Position -= new float3(1 * speedMultiplier, 0, 0);
                }
                else
                {
                    transformComp.ValueRW.Position -= new float3(0, 0, 0);
                }
            }
        }
        

        public void OnDestroy(ref SystemState state)
        {
            state.Enabled = false;
        }
    }
}
