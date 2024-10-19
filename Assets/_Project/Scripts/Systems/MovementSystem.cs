using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.Search;
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
<<<<<<< Updated upstream
                if (movementComp.ValueRO.vector.x > 1)
=======
                var movementComp = SystemAPI.GetComponent<MovementComponent>(e);
                var transformComp = SystemAPI.GetComponent<LocalTransform>(e);

                var speedMultiplier = Time.deltaTime * movementComp.speed;
                if (movementComp.vector.y > 0)
>>>>>>> Stashed changes
                {
                    movementComp.ValueRW.vector.x = 1;
                }
                else if (movementComp.ValueRO.vector.x < -1)
                {
                    movementComp.ValueRW.vector.x = -1;
                }
                
                if (movementComp.ValueRO.vector.y > 1)
                {
                    movementComp.ValueRW.vector.y = 1;
                }
                else if (movementComp.ValueRO.vector.y < -1)
                {
                    movementComp.ValueRW.vector.y = -1;
                }
                
                var speedMultiplier = Time.deltaTime * movementComp.ValueRO.speed;
                
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
