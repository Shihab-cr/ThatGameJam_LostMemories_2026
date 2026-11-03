using EJETAGame;
using Player.Interaction;
using UnityEngine;

public class PlayerCarryController : MonoBehaviour
{
    private Items currItemEnum;
    public KeyCode dropItemKey = KeyCode.G;
    private GameObject currItemObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dropItemKey) && currItemObject != null)
        {
            DropItem();
        }
    }
    public void DropItem()
    {
        if (currItemObject != null)
        {
            currItemObject.transform.SetParent(null);
            
            Rigidbody itemRb = currItemObject.GetComponent<Rigidbody>();
            PickupItem itemPickup = currItemObject.GetComponent<PickupItem>();
            
            if (itemPickup != null) itemPickup.enabled = true;
            if (itemRb != null)itemRb.isKinematic = false;
            
            currItemObject = null;
            currItemEnum = Items.None;
        }
    }
    public void SetCurrentItem(Items itemEnum, GameObject itemObject)
    {
        currItemEnum = itemEnum;
        currItemObject = itemObject;
    }
    public bool HasItem()
    {
        return currItemEnum != Items.None;
    }
    public Items GetCurrentItem()
    {
        return currItemEnum;
    }
    public GameObject GetCurrentItemObject()
    {
        return currItemObject;
    }
    public void RemoveItemFromHand()
    {
        currItemEnum = Items.None;
        currItemObject = null;
    }
}
