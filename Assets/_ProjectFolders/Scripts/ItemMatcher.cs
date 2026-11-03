namespace EJETAGame
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class ItemMatcher : MonoBehaviour
    {
        [Tooltip("The ID this pedestal needs to receive a correct match.")]
        [SerializeField] private string expectedID;

        private bool isCorrectMatch = false;
        public bool IsCorrectMatch => isCorrectMatch;

        [Header("Local Events (VFX/SFX)")]
        public UnityEvent OnMatchSuccess;
        public UnityEvent OnMatchFailed;

        // Internal C# Event for the PuzzleManager to monitor
        public event Action OnMatchStateChanged;

        // Called by PlacementSocket's OnObjectPlaced UnityEvent
        public void ValidatePlacedItem()
        {
            // Because PlacementSocket parents the item to a child "PlacementPoint",
            // we can easily find the placed item's ID by searching the hierarchy.
            PuzzleItemID placedItem = GetComponentInChildren<PuzzleItemID>();

            if (placedItem != null && placedItem.itemID == expectedID)
            {
                isCorrectMatch = true;
                OnMatchSuccess?.Invoke();
            }
            else
            {
                isCorrectMatch = false;
                OnMatchFailed?.Invoke();
            }

            OnMatchStateChanged?.Invoke();
        }

        private void Update()
        {
            // Automatically reset if the player picks the correct item back up
            if (isCorrectMatch && GetComponentInChildren<PuzzleItemID>() == null)
            {
                isCorrectMatch = false;
                OnMatchStateChanged?.Invoke();
            }
        }
    }
}