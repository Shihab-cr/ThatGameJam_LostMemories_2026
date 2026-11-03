namespace EJETAGame
{
    using UnityEngine;
    using UnityEngine.Events;

    public class MemoryShardManager : MonoBehaviour
    {
        [Header("Shard Stats")]
        [SerializeField] private int totalShardsInScene = 0;
        [SerializeField] private int collectedShards = 0;

        [Header("Global Events")]
        [Tooltip("Fires every time a single shard is collected.")]
        public UnityEvent OnAnyShardCollected;

        [Tooltip("Fires when the player collects the final shard in the map.")]
        public UnityEvent OnAllShardsCollected;

        void Start()
        {
            // Dynamically find and count all shards in the scene on startup
            MemoryShard[] shards = FindObjectsOfType<MemoryShard>();
            totalShardsInScene = shards.Length;
        }

        public void AddCollectedShard()
        {
            collectedShards++;
            OnAnyShardCollected?.Invoke();

            if (collectedShards >= totalShardsInScene && totalShardsInScene > 0)
            {
                OnAllShardsCollected?.Invoke();
            }
        }

        // Public getters in case you want to link this to a UI script later
        public int GetTotalShards() => totalShardsInScene;
        public int GetCollectedShards() => collectedShards;
    }
}