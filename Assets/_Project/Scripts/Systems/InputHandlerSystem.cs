using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ECS
{
    public partial struct InputHandlerSystem : ISystem
    {
        private EntityQuery inputQuery;
        EntityQuery movementQuery;
        public void OnCreate(ref SystemState state)
        {
            inputQuery = state.GetEntityQuery(ComponentType.ReadOnly<InputComponent>());
            movementQuery = state.GetEntityQuery(ComponentType.ReadWrite<MovementComponent>());
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var entity = inputQuery.GetSingletonEntity();
            var inputComponent = SystemAPI.GetComponent<InputComponent>(entity);

            foreach (var e in movementQuery.ToEntityArray(Allocator.Temp))
            {
                var movementComponent = SystemAPI.GetComponent<MovementComponent>(e);
                
                switch (inputComponent.keyDown)
                {
                    case (int)KeyCode.W:
                        movementComponent.vector += new float3(0,1,0);
                        break;
                    case (int)KeyCode.A:
                        movementComponent.vector += new float3(-1,0,0);
                        break;
                    case (int)KeyCode.S:
                        movementComponent.vector += new float3(0,-1,0);
                        break;
                    case (int)KeyCode.D:
                        movementComponent.vector += new float3(1,0,0);
                        break;
                }
                switch (inputComponent.keyUp)
                {
                    case (int)KeyCode.W:
                        movementComponent.vector += new float3(0,-1,0);
                        break;
                    case (int)KeyCode.A:
                        movementComponent.vector += new float3(1,0,0);
                        break;
                    case (int)KeyCode.S:
                        movementComponent.vector += new float3(0,1,0);
                        break;
                    case (int)KeyCode.D:
                        movementComponent.vector += new float3(-1,0,0);
                        break;
                }
                
                state.EntityManager.SetComponentData(e, movementComponent);
            }
        }
        
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}