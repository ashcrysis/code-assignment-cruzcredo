namespace Player
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerInputReader : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public bool AttackPressed { get; private set; }
        public bool DashPressed { get; private set; }

        PlayerInput input;

        void Awake()
        {
            input = GetComponent<PlayerInput>();
        }

        void OnEnable()
        {
            input.actions["Move"].performed += OnMove;
            input.actions["Move"].canceled += OnMove;

            input.actions["Attack"].performed += OnAttack;
            input.actions["Dash"].performed += OnDash;
        }
        

        void OnMove(InputAction.CallbackContext ctx)
        {
            MoveInput = ctx.ReadValue<Vector2>();
        }

        void OnAttack(InputAction.CallbackContext ctx)
        {
            AttackPressed = true;
        }

        void OnDash(InputAction.CallbackContext ctx)
        {
            DashPressed = true;
        }

        public void ResetInput()
        {
            AttackPressed = false;
            DashPressed = false;
        }
    }
}