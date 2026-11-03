

namespace EJETAGame
{
    using Player.Interaction;
    using UnityEngine;
    using System;
    using UnityEngine.Events;

    public class ItemReceiver : MonoBehaviour, IInteractable
    {
        [SerializeField] private Items requiredTool;
        [SerializeField] private PlayerCarryController carryController; // Reference to the player's hand logic
        public KeyCode interactionKey = KeyCode.E; // Key to trigger the interaction
        public UnityEvent OnToolUsed; // Event to notify when the tool is used
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (carryController == null)
            {
                carryController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCarryController>();
            }

        }
        public void Interact()
        {
            if(Input.GetKeyDown(interactionKey))
            {
                if(carryController.HasItem() && carryController.GetCurrentItem() == requiredTool)
                {
                    // Logic for using the tool goes here
                    Debug.Log("Using the tool: " + requiredTool);
                    OnToolUsed?.Invoke();
                }
            }
        }
        public void OnInteractEnter()
        {
            InteractionText.instance.SetText(interactionKey.ToString());
        }
        public void OnInteractExit()
        {
        }
    }
}

