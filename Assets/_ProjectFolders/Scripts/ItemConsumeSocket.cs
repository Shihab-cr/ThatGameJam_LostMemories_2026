namespace EJETAGame
{
    using Player.Interaction;
    using UnityEngine;
    using UnityEngine.Events;

    public class ItemConsumeSocket : MonoBehaviour, IInteractable
    {
        [SerializeField] private Items requiredItem;
        [SerializeField] private PlayerCarryController carryController;

        [Header("State Changes")]
        [Tooltip("The static mesh already placed perfectly on the table/pedestal (starts inactive).")]
        [SerializeField] private GameObject visualItemToActivate;

        [Tooltip("The next interaction script to enable (e.g., a valve to turn or button to press).")]
        [SerializeField] private MonoBehaviour nextInteractionScript;

        public KeyCode interactionKey = KeyCode.E;
        [SerializeField] private string interactPrompt = "Place";

        public UnityEvent OnItemConsumedEvent;

        void Start()
        {
            if (carryController == null)
            {
                carryController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCarryController>();
            }

            // Ensure the next script starts disabled to enforce the puzzle sequence
            if (nextInteractionScript != null)
            {
                nextInteractionScript.enabled = false;
            }
        }

        public void Interact()
        {
            if (Input.GetKeyDown(interactionKey))
            {
                if (carryController.HasItem() && carryController.GetCurrentItem() == requiredItem)
                {
                    // 1. Destroy the physical item the player is carrying
                    GameObject heldItem = carryController.GetCurrentItemObject();
                    carryController.RemoveItemFromHand(); // Clear internal player state
                    Destroy(heldItem); // Completely remove it from the scene

                    // 2. Enable the perfectly positioned visual fake
                    if (visualItemToActivate != null)
                    {
                        visualItemToActivate.SetActive(true);
                    }

                    // 3. Trigger your PlaySFX and PlayVFX scripts
                    OnItemConsumedEvent?.Invoke();

                    // 4. Enable the next puzzle step
                    if (nextInteractionScript != null)
                    {
                        nextInteractionScript.enabled = true;
                    }

                    // 5. Permanently disable this script so it cannot be triggered again
                    this.enabled = false;
                }
            }
        }

        public void OnInteractEnter()
        {
            // Only show the placement prompt if they are holding the correct item
            if (carryController.HasItem() && carryController.GetCurrentItem() == requiredItem)
            {
                InteractionText.instance.SetText(interactPrompt + " " + interactionKey.ToString());
            }
        }

        public void OnInteractExit() { }
    }
}