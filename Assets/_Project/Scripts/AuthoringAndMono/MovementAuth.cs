using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public class MovementAuth : MonoBehaviour
    {
        [SerializeField] Vector3 derection;
        [SerializeField] float moveSpeed;

        class MovementAuthBaker : Baker<MovementAuth>
        {
            public override void Bake(MovementAuth authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new MovementComponent
                {
                    vector = authoring.derection,
                    speed = authoring.moveSpeed,
                });
            }
        }
    }
}

