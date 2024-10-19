using Unity.Entities;
using Unity.Mathematics;

namespace ECS
{
    public struct MovementComponent : IComponentData
    {
        public float3 vector;
        public float speed;
    }
}

