using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    [UpdateAfter(typeof(InputSystem))]
    [CreateAfter(typeof(InputSystem))]
    public partial struct MovementSystem : ISystem
    {
        private EntityQuery _entityQuery;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputComponent>();
            _entityQuery = state.GetEntityQuery(
                ComponentType.ReadWrite<MovementComponent>(),
                ComponentType.ReadWrite<LocalTransform>(),
                ComponentType.ReadOnly<ControlEntityComponent>()
            );
        }

        public void OnUpdate(ref SystemState state)
        {
            var inputEntity = SystemAPI.GetSingletonEntity<InputComponent>();
            var inputcomp = SystemAPI.GetComponent<InputComponent>(inputEntity);
            
            foreach (var e in _entityQuery.ToEntityArray(Allocator.Temp))
            {
                var movementComp = SystemAPI.GetComponent<MovementComponent>(e);
                var transformComp = SystemAPI.GetComponent<LocalTransform>(e);

                var speedMultiplier = Time.deltaTime * movementComp.speed;
                Debug.Log(speedMultiplier);
                
                if (movementComp.vector.y > 0)
                {
                    transformComp.Position += new float3(0, 1 * speedMultiplier, 0);
                }
                else if (movementComp.vector.y < 0)
                {
                    transformComp.Position -= new float3(0, 1 * speedMultiplier, 0);
                }

                if (movementComp.vector.x > 0)
                {
                    transformComp.Position += new float3(1 * speedMultiplier, 0, 0);
                }
                else if (movementComp.vector.x < 0)
                {
                    transformComp.Position -= new float3(1 * speedMultiplier, 0, 0);
                }
                
                state.EntityManager.SetComponentData(e, transformComp);
                
            }
        }
        

        public void OnDestroy(ref SystemState state)
        {
            state.Enabled = false;
        }
    }
}
