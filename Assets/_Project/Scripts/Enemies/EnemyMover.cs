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
        [SerializeField] private float moveSpeed =2.8f;
        [SerializeField] private float zigZagOffset =0.8f;
        [SerializeField] private float zigZagForwardDistance =3f;
        [SerializeField] private float repathInterval =0.25f;

        private NavMeshAgent agent;
        private IMovementStrategy movementStrategy;
        private float nextRepathTime;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
            agent.acceleration =18f;
            agent.angularSpeed =480f;
            agent.stoppingDistance =0.25f;
            agent.autoBraking =false;
            agent.updateRotation = true;
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;

            SetStrategy();
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            agent.speed =moveSpeed;

            if (Time.time < nextRepathTime)
            {
                return;
            }

            nextRepathTime = Time.time+repathInterval;

            Vector3 destination = target.position;

            //make zigzag smoother with a small side movement
            if (movementType == MovementType.ZigZag)
            {
                Vector3 direction = movementStrategy.GetDirection(transform, target);
                Vector3 side = Vector3.Cross(Vector3.up, direction).normalized;
                float wave = Mathf.Sin(Time.time * 1.4f);

                destination =transform.position;
                destination +=direction * zigZagForwardDistance;
                destination +=side * wave * zigZagOffset;
            }

            //keep the destination on the baked navmesh
            if (NavMesh.SamplePosition(destination, out NavMeshHit hit,3f,NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target =newTarget;
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