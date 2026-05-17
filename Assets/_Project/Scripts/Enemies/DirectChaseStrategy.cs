using CoreBreach.Interfaces;
using UnityEngine;

namespace CoreBreach.Enemies
{
    public class DirectChaseStrategy : IMovementStrategy
    {
        public Vector3 GetDirection(Transform owner, Transform target)
        {
            Vector3 direction =target.position - owner.position;
            direction.y =0f;
            return direction.normalized;
        }
    }
}
