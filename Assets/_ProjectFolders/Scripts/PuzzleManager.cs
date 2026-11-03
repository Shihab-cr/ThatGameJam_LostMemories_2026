namespace EJETAGame
{
    using UnityEngine;
    using UnityEngine.Events;

    public class PuzzleManager : MonoBehaviour
    {
        [Tooltip("Drag all the ItemMatcher scripts for this specific puzzle here.")]
        [SerializeField] private ItemMatcher[] managedPedestals;

        [Header("Puzzle Solved Events")]
        [Tooltip("Fires once when all pedestals contain their correct items.")]
        public UnityEvent OnPuzzleSolved;

        private bool isPuzzleSolved = false;

        private void OnEnable()
        {
            foreach (var matcher in managedPedestals)
            {
                if (matcher != null) matcher.OnMatchStateChanged += CheckPuzzleState;
            }
        }

        private void OnDisable()
        {
            foreach (var matcher in managedPedestals)
            {
                if (matcher != null) matcher.OnMatchStateChanged -= CheckPuzzleState;
            }
        }

        private void CheckPuzzleState()
        {
            if (isPuzzleSolved) return;

            // If any pedestal is currently wrong or empty, cancel the check
            foreach (var matcher in managedPedestals)
            {
                if (!matcher.IsCorrectMatch) return;
            }

            // If the loop completes, all items are correct
            isPuzzleSolved = true;
            OnPuzzleSolved?.Invoke();
        }
    }
}