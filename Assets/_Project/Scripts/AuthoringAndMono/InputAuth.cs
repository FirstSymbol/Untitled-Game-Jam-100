using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public class InputAuth : MonoBehaviour
    {
        public KeyCode keyDown;
        public KeyCode keyUp;
        public KeyCode keyPressed;

        class InputBaker : Baker<InputAuth>
        {
            public override void Bake(InputAuth authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent(entity, new InputComponent()
                {
                    keyDown = (int)authoring.keyDown,
                    keyUp = (int)authoring.keyUp,
                    keyPressed = (int)authoring.keyPressed
                });
            }
        }
    }
}