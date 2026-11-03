using Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Interaction
{
    public class Interaction : MonoBehaviour
    {
        InputController input;
        InputAction interactAction;

        [Header("Interaction")]
        [SerializeField] Camera firstPersonCamera;
        [SerializeField] float interactDistance = 3f;
        [SerializeField] LayerMask interactLayer;
        [SerializeField] Items currentItem = Items.None;

        public Items CurrentItem;

        //------------------------------------------------

        void Awake()
        {
            input = GetComponent<InputController>();
            interactAction = input.GetInteraction();
        }

        void Update()
        {
            if (interactAction.WasPressedThisFrame())
                TryInteract();
        }

        //------------------------------------------------

        void TryInteract()
        {
            Ray ray = firstPersonCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            // Only the first hit
            if (!Physics.Raycast(ray, out RaycastHit hit, interactDistance))
                return;

            if (((1 << hit.collider.gameObject.layer) & interactLayer) == 0)
                return;

            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log("Interaction found: " + hit.collider.name);
                interactable.Interact(this);
            }
        }
    }

    public enum Items
    {
        None,
        Shovel,
        Stick
    }
}