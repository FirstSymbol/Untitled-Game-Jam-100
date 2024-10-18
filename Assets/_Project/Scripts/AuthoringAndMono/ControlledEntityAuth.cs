using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public class ControlledEntityAuth : MonoBehaviour
    {
        private class ControlledEntityAuthBaker : Baker<ControlledEntityAuth>
        {
            public override void Bake(ControlledEntityAuth authoring)
            {
                Entity entity = GetEntity(authoring,TransformUsageFlags.None);
                AddComponent<ControlEntityComponent>(entity);
            }
        }
    }
}