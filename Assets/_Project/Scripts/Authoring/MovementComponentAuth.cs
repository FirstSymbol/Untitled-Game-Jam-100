using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public class MovementComponentAuth : MonoBehaviour
    {
        [SerializeField] Vector3 derection;
        [SerializeField] float moveSpeed;

        class MovementAuthBaker : Baker<MovementComponentAuth>
        {
            public override void Bake(MovementComponentAuth authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MovementComponent
                {
                    vector = authoring.derection,
                    speed = authoring.moveSpeed,
                });
            }
        }
    }
}

