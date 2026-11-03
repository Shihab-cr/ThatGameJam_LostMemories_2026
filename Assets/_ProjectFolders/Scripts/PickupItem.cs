

namespace EJETAGame
{
    using Player.Interaction;
    using UnityEngine;
    public class PickupItem : MonoBehaviour, IInteractable
    {
        public KeyCode interactionKey = KeyCode.E;
        [SerializeField] private GameObject itemHoldParent;
        [SerializeField] private Items currentItem;
        [SerializeField] private string pickupText = "Pickup";
        [SerializeField] private PlayerCarryController carryController;
        [SerializeField] private Quaternion itemPickupRotation = Quaternion.Euler(0, 0, 0);
        Rigidbody itemRb;
        private void Start()
        {
            itemRb = GetComponent<Rigidbody>();
            if(carryController == null)
            {
                carryController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCarryController>();
            }   
        }
        public void Interact()
        {
            if (Input.GetKeyDown(interactionKey))
            {
                if (carryController.HasItem()) return;
                if(itemRb != null) itemRb.isKinematic = true;
                transform.SetParent(itemHoldParent.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = itemPickupRotation;
                carryController.SetCurrentItem(currentItem, gameObject);
                this.enabled = false;
            }
        }
        public void OnInteractEnter()
        {
            InteractionText.instance.SetText(pickupText + " " + interactionKey.ToString());
        }
        public void OnInteractExit()
        {

        }
    }

}
