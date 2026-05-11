using UnityEngine;

namespace CoreBreach.Interfaces
{
    public interface IMovementStrategy
    {
        Vector3 GetDirection(Transform owner, Transform target);
    }
}
