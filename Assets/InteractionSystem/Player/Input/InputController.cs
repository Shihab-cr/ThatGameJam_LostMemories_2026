using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class InputController : MonoBehaviour
    {
        public static InputController Instance { get; private set; }
        InputSystem_Actions inputActions;

        //------------------------------------------------------------

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            inputActions = new InputSystem_Actions();
            Instance = this;
        }

        void Start()
        {
            inputActions.Player.Enable();
        }

        //------------------------------------------------------------

        public InputAction GetMove() => inputActions.Player.Move;
        public InputAction GetLook() => inputActions.Player.Look;
        public InputAction GetInteraction() => inputActions.Player.Interact;

        //------------------------------------------------------------

        public void SetAll(bool value)
        {
            if (value)
                inputActions.Player.Enable();
            else
                inputActions.Player.Disable();
        }

        public void SetMoveAction(bool value)
        {
            if (value)
                inputActions.Player.Move.Enable();
            else
                inputActions.Player.Move.Disable();
        }

        public void SetLookAction(bool value)
        {
            if (value)
                inputActions.Player.Look.Enable();
            else
                inputActions.Player.Look.Disable();
        }

        public void SetInteractAction(bool value)
        {
            if (value)
                inputActions.Player.Interact.Enable();
            else
                inputActions.Player.Interact.Disable();
        }
    }
}