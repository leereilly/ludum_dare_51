using UnityEngine;
using UnityEngine.InputSystem;

namespace Helzinko
{
    public class PlayerController : MonoBehaviour
    {
        PlayerInput playerInput;

        private Input input;

        public float movement { private set; get; }

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            input = new Input();
            input.Player.Enable();

            input.Player.Movement.performed += Movement;
            input.Player.Movement.canceled += Movement;
        }

        private void Movement(InputAction.CallbackContext obj)
        {
            movement = obj.ReadValue<Vector2>().normalized.x;
        }

        public bool Jump()
        {
            return input.Player.Jump.triggered;
        }

        private void OnDestroy()
        {
            input.Player.Movement.performed -= Movement;
            input.Player.Movement.canceled -= Movement;
        }
    }
}
