using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreBreach.Player
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 0.12f;
        [SerializeField] private bool lockCursorOnStart = true;

        private float yaw;

        private void Start()
        {
            yaw = transform.eulerAngles.y;

            if (lockCursorOnStart)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void Update()
        {
            if (Mouse.current == null)
            {
                return;
            }

            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            yaw += mouseDelta.x * mouseSensitivity;
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);

            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
