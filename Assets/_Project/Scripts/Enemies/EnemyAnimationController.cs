using UnityEngine;
using UnityEngine.AI;

namespace CoreBreach.Enemies
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float movementThreshold =0.1f;

        private bool hasX;
        private bool hasY;
        private bool hasSpeed;
        private bool hasMoveSpeed;
        private bool hasIsMoving;

        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }

            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }

            CacheParameters();
        }

        private void Update()
        {
            if (animator == null || agent == null || animator.runtimeAnimatorController == null)
            {
                return;
            }

            Vector3 localVelocity =transform.InverseTransformDirection(agent.velocity);
            float speed =agent.velocity.magnitude;
            bool isMoving  = speed > movementThreshold;

            //send movement values only if the animator has them
            if (hasX) animator.SetFloat("X",localVelocity.x);
            if (hasY) animator.SetFloat("Y",localVelocity.z);
            if (hasSpeed) animator.SetFloat("Speed",speed);
            if (hasMoveSpeed) animator.SetFloat("MoveSpeed",speed);
            if (hasIsMoving) animator.SetBool("IsMoving",isMoving);
        }

        private void CacheParameters()
        {
            if (animator == null || animator.runtimeAnimatorController == null)
            {
                return;
            }

            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.name =="X") hasX =true;
                if (parameter.name =="Y") hasY = true;
                if (parameter.name =="Speed") hasSpeed =true;
                if (parameter.name =="MoveSpeed") hasMoveSpeed =true;
                if (parameter.name =="IsMoving") hasIsMoving =true;
            }
        }
    }
}