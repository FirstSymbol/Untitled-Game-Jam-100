using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;


namespace ECS
{
    
    
    public partial struct InputSystem : ISystem
    {
        private NativeArray<int> keys;
        
        
        public void OnCreate(ref SystemState state)
        {
            keys = new NativeArray<int>(4, Allocator.TempJob);
            keys[0] = (int)KeyCode.W;
            keys[1] = (int)KeyCode.A;
            keys[2] = (int)KeyCode.S;
            keys[3] = (int)KeyCode.D;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            
            foreach (var inputComponent in SystemAPI.Query<RefRW<InputComponent>>())
            {
                inputComponent.ValueRW.keyUp = (int)KeyCode.None;
                inputComponent.ValueRW.keyDown = (int)KeyCode.None;
                inputComponent.ValueRW.keyPressed = (int)KeyCode.None;
                foreach (var k in keys)
                {
                    if (Input.GetKeyDown((KeyCode) k))
                    {
                        inputComponent.ValueRW.keyDown = k;
                    }
                    if (Input.GetKeyUp((KeyCode) k))
                    {
                        inputComponent.ValueRW.keyUp = k;
                    }

                    if (Input.GetKey((KeyCode)k))
                    {
                        inputComponent.ValueRW.keyPressed = k;
                    }
                }
                
            }
            
        }

        
        public void OnDestroy(ref SystemState state)
        {
            state.Enabled = false;
        }
    }
}