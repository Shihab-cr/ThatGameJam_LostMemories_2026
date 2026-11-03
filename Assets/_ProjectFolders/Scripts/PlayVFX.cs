using UnityEngine;

[System.Serializable]
public struct VFXGroup
{
    public string groupName; // e.g., "Dust Puff", "Magical Sparkle"
    public ParticleSystem[] vfxPrefabs;
}

public class PlayVFX : MonoBehaviour
{
    [SerializeField] private VFXGroup[] vfxGroups;
    [SerializeField] private Transform spawnPos;
    private bool canSpawnVFX = true;

    public void PlayRandomVFX(int groupIndex)
    {
        if (canSpawnVFX && groupIndex >= 0 && groupIndex < vfxGroups.Length)
        {
            ParticleSystem[] prefabs = vfxGroups[groupIndex].vfxPrefabs;
            if (prefabs.Length > 0)
            {
                canSpawnVFX = false;

                int randomIndex = Random.Range(0, prefabs.Length);
                ParticleSystem randomVFX = prefabs[randomIndex];
                Vector3 targetPos = spawnPos != null ? spawnPos.position : transform.position;

                ParticleSystem spawnedPS = Instantiate(randomVFX, targetPos, Quaternion.identity);
                GameObject vfxObject = spawnedPS.gameObject;

                AutoDestroyParticleSystem autoDestroy = vfxObject.AddComponent<AutoDestroyParticleSystem>();
                autoDestroy.OnParticleSystemFinished += SetCanSpawnVFX;
            }
        }
    }

    private void SetCanSpawnVFX(AutoDestroyParticleSystem source)
    {
        if (source != null)
        {
            source.OnParticleSystemFinished -= SetCanSpawnVFX;
        }
        canSpawnVFX = true;
    }
}