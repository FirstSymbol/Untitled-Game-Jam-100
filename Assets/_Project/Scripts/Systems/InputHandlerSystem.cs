using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ECS
{
    [UpdateAfter(typeof(InputSystem))]
    [CreateAfter(typeof(InputSystem))]
    public partial struct InputHandlerSystem : ISystem
    {
        //public void OnCreate(ref SystemState state)
        //{
        //}
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var inputQuery = state.GetEntityQuery(ComponentType.ReadOnly<InputComponent>());
            var inputComponent = inputQuery.GetSingleton<InputComponent>();

            foreach (var movementComponent in SystemAPI.Query<RefRW<MovementComponent>>())
            {
                switch (inputComponent.keyDown)
                {
                    case (int)KeyCode.W:
                            movementComponent.ValueRW.vector.y += 1;
                        break;
                    case (int)KeyCode.A:
                            movementComponent.ValueRW.vector.x -= 1f;
                        break;
                    case (int)KeyCode.S:
                            movementComponent.ValueRW.vector.y -= 1f;
                        break;
                    case (int)KeyCode.D:
                            movementComponent.ValueRW.vector.x += 1f;
                        break;
                }
                switch (inputComponent.keyUp)
                {
                    case (int)KeyCode.W:
                            movementComponent.ValueRW.vector.y -= 1f;
                        break;
                    case (int)KeyCode.A:
                            movementComponent.ValueRW.vector.x += 1f;
                        break;
                    case (int)KeyCode.S:
                            movementComponent.ValueRW.vector.y += 1f;
                        break;
                    case (int)KeyCode.D:
                            movementComponent.ValueRW.vector.x -= 1f;
                        break;
                }
            }
        }
        
        //public void OnDestroy(ref SystemState state)
        //{
        //    state.Enabled = false;
        //}
    }
}