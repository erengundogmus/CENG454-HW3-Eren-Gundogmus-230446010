using CoreBreach.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace CoreBreach.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        private enum MovementType
        {
            Direct,
            ZigZag
        }

        [SerializeField] private Transform target;
        [SerializeField] private MovementType movementType =MovementType.Direct;
        [SerializeField] private float moveSpeed =3.5f;
        [SerializeField] private float zigZagOffset =1.2f;
        [SerializeField] private float zigZagForwardDistance =2.5f;
        [SerializeField] private float zigZagSwitchTime =0.7f;
        [SerializeField] private float repathInterval =0.2f;

        private NavMeshAgent agent;
        private IMovementStrategy movementStrategy;
        private float nextRepathTime;
        private float nextZigZagTime;
        private float zigZagSide =1f;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
            agent.acceleration =80f;
            agent.angularSpeed =720f;
            agent.stoppingDistance =0.2f;
            agent.autoBraking =false;
            agent.updateRotation = true;
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

            SetStrategy();
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            agent.speed = moveSpeed;

            if (Time.time >= nextZigZagTime)
            {
                zigZagSide *= -1f;
                nextZigZagTime = Time.time + zigZagSwitchTime;
            }

            if (Time.time < nextRepathTime)
            {
                return;
            }

            nextRepathTime = Time.time + repathInterval;

            Vector3 destination = target.position;

            //make zigzag enemy move to alternating side points
            if (movementType == MovementType.ZigZag)
            {
                Vector3 direction = movementStrategy.GetDirection(transform, target);
                Vector3 side = Vector3.Cross(Vector3.up, direction).normalized;

                destination = transform.position;
                destination += direction * zigZagForwardDistance;
                destination += side * zigZagSide * zigZagOffset;
            }

            if (NavMesh.SamplePosition(destination, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            nextRepathTime =0f;
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