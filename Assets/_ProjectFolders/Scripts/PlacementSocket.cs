

namespace EJETAGame
{
    using System;
    using Player.Interaction;
    using UnityEngine;
    using UnityEngine.Events;
    public class PlacementSocket : MonoBehaviour
    {
        
        [SerializeField] private ItemReceiver itemReceiver; // Reference to the UseToolLogic script
        [SerializeField] private Transform placementPoint; // Point where the object will be placed
        
        private bool hasObjectPlaced = false;
        public UnityEvent OnObjectPlaced; // Event to notify when the object is placed
        [SerializeField] private PlayerCarryController carryController; // Reference to the player's hand logic
        private Collider objectCollider;
        void Awake()
        {
            objectCollider = GetComponent<Collider>();
            if (itemReceiver == null) itemReceiver = GetComponent<ItemReceiver>();

            // FIX: You cannot set the position of a null Transform. You must create one.
            if (placementPoint == null)
            {
                GameObject point = new GameObject("PlacementPoint");
                point.transform.SetParent(transform);
                Vector3 defaultPos = objectCollider != null
            ? new Vector3(objectCollider.bounds.center.x, objectCollider.bounds.max.y, objectCollider.bounds.center.z)
            : transform.position;

                point.transform.position = defaultPos;
                placementPoint = point.transform;
            }
        }
        void Start()
        {
            if (carryController == null)
            {
                carryController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCarryController>();
            }
            if(itemReceiver == null)
            {
                itemReceiver = GetComponent<ItemReceiver>();
            }
            objectCollider = GetComponent<Collider>();
            
        }
        private void OnEnable()
        {
            if(itemReceiver != null)
            {
                itemReceiver.OnToolUsed.AddListener(PlaceItem);
            }
        }
        private void OnDisable()
        {
            if(itemReceiver != null)
            {
                itemReceiver.OnToolUsed.RemoveListener(PlaceItem);
            }
        }
        public void PlaceItem()
        {
            if (!hasObjectPlaced && carryController.HasItem())
            {
                GameObject itemToPlace = carryController.GetCurrentItemObject();
                itemToPlace.transform.position = placementPoint.position;
                itemToPlace.transform.rotation = placementPoint.rotation;
                itemToPlace.transform.SetParent(placementPoint);
                
                carryController.RemoveItemFromHand();
                PickupItem itemPickupScript = itemToPlace.GetComponent<PickupItem>();
                if(itemPickupScript != null)
                {
                    itemPickupScript.enabled = true; 
                }
                hasObjectPlaced = true;
                OnObjectPlaced?.Invoke(); // Notify subscribers that the object has been placed
            }
            if (hasObjectPlaced && !carryController.HasItem())
            {
                hasObjectPlaced = false;
            }
        }
        void Update()
        {
            // If the table thinks it has an object, but the object was picked up (unparented)
            if (hasObjectPlaced && placementPoint.childCount == 0)
            {
                hasObjectPlaced = false;
            }
        }
    }

}
