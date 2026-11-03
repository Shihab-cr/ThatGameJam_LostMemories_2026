using EJETAGame;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab; // The prefab of the item to spawn
    [SerializeField] private ItemReceiver itemReceiver; // Reference to the UseToolLogic script
    [SerializeField] private Transform spawnPos;
    private bool hasItemSpawned = false; // Flag to check if the item has already been spawned
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (itemReceiver == null)
        {
            itemReceiver = GetComponent<ItemReceiver>();
        }
    }
    private void OnEnable()
    {
        if (itemReceiver != null)
        {
            itemReceiver.OnToolUsed.AddListener(SpawnItemAtPosition);
        }
    }
    private void OnDisable()
    {
        if (itemReceiver != null)
        {
            itemReceiver.OnToolUsed.RemoveListener(SpawnItemAtPosition);
        }
    }
    public void SpawnItemAtPosition()
    {
        if (itemPrefab != null && !hasItemSpawned)
        {
            // Fallback to the current object's position if spawnPos is null
            Vector3 targetPos = spawnPos != null ? spawnPos.position : transform.position;
            Instantiate(itemPrefab, targetPos, Quaternion.identity);
            hasItemSpawned = true;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
