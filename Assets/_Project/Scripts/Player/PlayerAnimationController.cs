using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreBreach.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float animationSmoothTime =0.1f;

        private float currentX;
        private float currentY;

        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        private void Update()
        {
            if (animator == null || Keyboard.current == null)
            {
                return;
            }

            float inputX =0f;
            float inputY =0f;

            if (Keyboard.current.wKey.isPressed)  inputY += 1f;
            if (Keyboard.current.sKey.isPressed) inputY -=1f;
            if (Keyboard.current.dKey.isPressed) inputX +=1f;
            if (Keyboard.current.aKey.isPressed) inputX -=1f;

            currentX =Mathf.Lerp(currentX,inputX,animationSmoothTime);
            currentY =Mathf.Lerp(currentY,inputY,animationSmoothTime);

            float speed =new Vector2(inputX,inputY).magnitude;

            //use existing soldier controller parameters
            animator.SetFloat("X",currentX);
            animator.SetFloat("Y",currentY);
            animator.SetFloat("Speed",speed);
            animator.SetFloat("MoveSpeed",speed);
        }
    }
}