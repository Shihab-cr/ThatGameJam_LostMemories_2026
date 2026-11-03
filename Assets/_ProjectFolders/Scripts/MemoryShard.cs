namespace EJETAGame
{
    using Player.Interaction;
    using UnityEngine;
    using UnityEngine.Events;

    public class MemoryShard : MonoBehaviour, IInteractable
    {
        public KeyCode interactionKey = KeyCode.E;
        [SerializeField] private string interactPrompt = "Collect Memory";

        [Tooltip("Automatically finds the manager on the Player if left blank.")]
        [SerializeField] private MemoryShardManager shardManager;

        [Header("Local Events (VFX/SFX)")]
        public UnityEvent OnShardCollectedEvent;

        void Start()
        {
            if (shardManager == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    shardManager = player.GetComponent<MemoryShardManager>();
                }
            }
        }

        public void Interact()
        {
            if (Input.GetKeyDown(interactionKey))
            {
                // 1. Trigger local VFX/SFX
                OnShardCollectedEvent?.Invoke();

                // 2. Update the global count
                if (shardManager != null)
                {
                    shardManager.AddCollectedShard();
                }

                // 3. Remove the shard from the world
                Destroy(gameObject);
            }
        }

        public void OnInteractEnter()
        {
            InteractionText.instance.SetText(interactPrompt + " " + interactionKey.ToString());
        }

        public void OnInteractExit()
        {
        }
    }
}