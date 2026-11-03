using Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Look : MonoBehaviour
    {
        InputController input;
        InputAction lookAction;

        [Header("Look Settings")]
        [SerializeField] Camera firstPersonCamera;

        [SerializeField] float mouseSensitivity = 0.1f;
        [SerializeField] float angleLookDown = 90f;
        [SerializeField] float angleLookUp = -90f;

        float pitch;

        //------------------------------------------------

        void Awake()
        {
            input = GetComponent<InputController>();
            lookAction = input.GetLook();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update() => LookAround();

        //------------------------------------------------

        void LookAround()
        {
            Vector2 lookInput = lookAction.ReadValue<Vector2>() * mouseSensitivity;

            // LOOK UP / DOWN
            pitch = Mathf.Clamp(pitch - lookInput.y, angleLookUp, angleLookDown);
            firstPersonCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

            // TURN LEFT / RIGHT
            transform.Rotate(Vector3.up * lookInput.x);
        }
    }
}