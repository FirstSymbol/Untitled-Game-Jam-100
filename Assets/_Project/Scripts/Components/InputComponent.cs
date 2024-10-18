using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public struct InputComponent : IComponentData
    {
        public int keyDown;
        public int keyUp;
        public int keyPressed;
    }
}