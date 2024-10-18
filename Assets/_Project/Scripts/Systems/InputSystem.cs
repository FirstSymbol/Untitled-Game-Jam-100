using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;


namespace ECS
{
    
    
    public partial struct InputSystem : ISystem
    {
        private EntityQuery _entityQuery;
        private NativeArray<int> keys;
        
        
        public void OnCreate(ref SystemState state)
        {
            keys = new NativeArray<int>(4, Allocator.TempJob);
            keys[0] = (int)KeyCode.W;
            keys[1] = (int)KeyCode.A;
            keys[2] = (int)KeyCode.S;
            keys[3] = (int)KeyCode.D;
            
            _entityQuery = state.GetEntityQuery(ComponentType.ReadWrite<InputComponent>());
        }

        
        public void OnUpdate(ref SystemState state)
        {
            
            foreach (var entity in _entityQuery.ToEntityArray(Allocator.Temp))
            {
                var inputcomponent = SystemAPI.GetComponent<InputComponent>(entity);
                inputcomponent.keyUp = (int)KeyCode.None;
                inputcomponent.keyDown = (int)KeyCode.None;
                inputcomponent.keyPressed = (int)KeyCode.None;
                foreach (var k in keys)
                {
                    if (Input.GetKeyDown((KeyCode) k))
                    {
                        inputcomponent.keyDown = k;
                    }
                    if (Input.GetKeyUp((KeyCode) k))
                    {
                        inputcomponent.keyUp = k;
                    }

                    if (Input.GetKey((KeyCode)k))
                    {
                        inputcomponent.keyPressed = k;
                    }
                    state.EntityManager.SetComponentData(entity,inputcomponent);
                }
                
            }
            
        }

        
        public void OnDestroy(ref SystemState state)
        {
            state.Enabled = false;
        }
    }
}