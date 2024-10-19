using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public class ControlledEntityTagAuth : MonoBehaviour
    {
        private class ControlledEntityAuthBaker : Baker<ControlledEntityTagAuth>
        {
            public override void Bake(ControlledEntityTagAuth authoring)
            {
                Entity entity = GetEntity(authoring,TransformUsageFlags.None);
                AddComponent<ControlEntityTag>(entity);
            }
        }
    }
}