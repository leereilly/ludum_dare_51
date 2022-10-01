using UnityEngine;
using UnityEngine.InputSystem;

namespace Helzinko
{
    public class PlayerController : MonoBehaviour
    {
        PlayerInput playerInput;

        private Input input;

        private Camera cam;

        public float movement { private set; get; }
        public Vector2 aimVector { private set; get; }

        public bool shooting { private set; get; }

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            input = new Input();
            input.Player.Enable();

            input.Player.Movement.performed += Movement;
            input.Player.Movement.canceled += Movement;

            input.Player.Aim.performed += UpdateAim;
            input.Player.Aim.canceled += context =>
            {
                shooting = false;
                aimVector = aimVector.normalized * 0.1f;
            };

            input.Player.Shoot.performed += item => shooting = true;
            input.Player.Shoot.canceled += item => shooting = false;
        }

        private void Start()
        {
            cam = Camera.main;
        }
        private void Update()
        {
            if (playerInput.currentControlScheme == "Keyboard")
            {
                Cursor.visible = true;

                Vector2 mousePosition = Mouse.current.position.ReadValue();
                Vector2 cursorWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                aimVector = (cursorWorldPosition - (Vector2)transform.position).normalized;

                //cursor.position = cursorWorldPosition;
            }
            else
            {
                Cursor.visible = false;
            }
        }

        private void Movement(InputAction.CallbackContext obj)
        {
            movement = obj.ReadValue<Vector2>().normalized.x;
        }

        private void UpdateAim(InputAction.CallbackContext obj)
        {
            aimVector = obj.ReadValue<Vector2>().normalized;
            shooting = aimVector.magnitude > 0.5f;
        }

        public bool Jump()
        {
            return input.Player.Jump.triggered;
        }
    }
}
