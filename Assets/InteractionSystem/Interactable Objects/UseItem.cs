using Player.Interaction;
using UnityEngine;

namespace ItemInteraction
{
    public class UseItem : MonoBehaviour, IInteractable
    {
        [SerializeField] Items requiredItem;

        //------------------------------------------------

        public void Interact(Interaction interact)
        {
            if (interact.CurrentItem != requiredItem)
            {
                Debug.Log($"Could not interact with item: {interact.CurrentItem}");
                return;
            }

            PerformInteraction();
        }

        protected virtual void PerformInteraction()
        {
            Debug.Log($"Interacted with {requiredItem}");
        }
    }
}
