using Player.Interaction;
using UnityEngine;

namespace ItemInteraction
{
    public class PickUpItem : MonoBehaviour, IInteractable
    {
        [SerializeField] Items item;

        //------------------------------------------------

        public void Interact(Interaction player)
        {
            player.CurrentItem = item;
            Destroy(gameObject);
        }
    }
}
