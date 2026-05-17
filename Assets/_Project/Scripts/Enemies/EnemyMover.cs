using CoreBreach.Interfaces;
using UnityEngine;

namespace CoreBreach.Enemies
{
    [RequireComponent(typeof(CharacterController))]
    public class EnemyMover : MonoBehaviour
    {
        private enum MovementType
        {
            Direct,
            ZigZag
        }

        [SerializeField] private Transform target;
        [SerializeField] private MovementType movementType =MovementType.Direct;
        [SerializeField] private float moveSpeed =2.5f;
        [SerializeField] private float gravity =-20f;

        private CharacterController controller;
        private IMovementStrategy movementStrategy;
        private Vector3 verticalVelocity;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            SetStrategy();
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            Vector3 direction = movementStrategy.GetDirection(transform, target);

            // face the movement direction
            if (direction.sqrMagnitude > 0.01f)
            {
                transform.rotation =Quaternion.LookRotation(direction);
            }

            controller.Move(direction*moveSpeed*Time.deltaTime);

            if (controller.isGrounded && verticalVelocity.y < 0f)
            {
                verticalVelocity.y = -2f;
            }

            verticalVelocity.y += gravity * Time.deltaTime;
            controller.Move(verticalVelocity *Time.deltaTime);
        }

        private void SetStrategy()
        {
            //pick movement behavior
            if (movementType == MovementType.ZigZag)
            {
                movementStrategy =new ZigZagMovementStrategy();
            }
            else
            {
                movementStrategy = new DirectChaseStrategy();
            }
        }
    }
}
