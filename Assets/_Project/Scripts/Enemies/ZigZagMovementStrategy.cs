using CoreBreach.Interfaces;
using UnityEngine;

namespace CoreBreach.Enemies
{
    public class ZigZagMovementStrategy : IMovementStrategy
    {
        public Vector3 GetDirection(Transform owner, Transform target)
        {
            Vector3 forward =target.position - owner.position;
            forward.y =0f;
            forward.Normalize();

            Vector3 side = Vector3.Cross(Vector3.up, forward);
            float wave = Mathf.Sin(Time.time * 4f);

            //mix forward movement with side movement
            Vector3 direction =forward + side*wave*0.45f;
            return direction.normalized;
        }
    }
}
