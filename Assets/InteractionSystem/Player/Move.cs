using Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Move : MonoBehaviour
    {
        InputController input;
        InputAction moveAction;
        CharacterController characterController;

        [Header("Movement")]
        [SerializeField] float moveSpeed = 1f;

        bool useGravity = true;
        const float gravityAcceleration = -9.81f;
        const float groundedStickVelocity = -2f;
        Vector3 velocity;

        //-----------------------------------------------

        void Awake()
        {
            input = GetComponent<InputController>();
            moveAction = input.GetMove();

            characterController = GetComponent<CharacterController>();
        }

        void Update() => MoveCharacter();

        //-----------------------------------------------

        void MoveCharacter()
        {
            Vector2 moveInput = Vector2.ClampMagnitude(moveAction.ReadValue<Vector2>(), 1f);
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
            moveDir = transform.TransformDirection(moveDir);

            // Gravitation
            if (useGravity)
                UpdateGravityVelocity();
            else
                velocity.y = 0f;

            if (characterController.enabled)
            {
                Vector3 horizontalMotion = moveDir * moveSpeed;
                Vector3 fullMotion = horizontalMotion + Vector3.up * velocity.y;

                characterController.Move(fullMotion * Time.deltaTime);
            }
        }

        //------------------------------------------------

        void UpdateGravityVelocity()
        {
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = groundedStickVelocity;
            }

            velocity.y += gravityAcceleration * Time.deltaTime;
        }
    }
}