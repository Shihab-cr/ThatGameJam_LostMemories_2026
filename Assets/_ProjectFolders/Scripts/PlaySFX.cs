using UnityEngine;

[System.Serializable]
public struct SFXGroup
{
    public string groupName; // e.g., "Simple Interactions", "Success"
    public AudioClip[] clips;
}

public class PlaySFX : MonoBehaviour
{
    [SerializeField] private SFXGroup[] sfxGroups;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSFX(int groupIndex)
    {
        // Safety check to prevent IndexOutOfRangeException
        if (audioSource != null && groupIndex >= 0 && groupIndex < sfxGroups.Length)
        {
            AudioClip[] clips = sfxGroups[groupIndex].clips;
            if (clips.Length > 0)
            {
                int randomIndex = Random.Range(0, clips.Length);
                audioSource.PlayOneShot(clips[randomIndex]);
            }
        }
    }
}