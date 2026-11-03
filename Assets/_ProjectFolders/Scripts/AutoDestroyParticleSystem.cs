using System;
using System.Collections;
using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour
{
    // FIX: Pass the script instance through the event
    public event Action<AutoDestroyParticleSystem> OnParticleSystemFinished;

    private ParticleSystem m_particleSystem;

    void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
        if (m_particleSystem != null)
        {
            StartCoroutine(DestroyWhenFinished());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyWhenFinished()
    {
        yield return new WaitForSeconds((m_particleSystem.main.duration + m_particleSystem.main.startLifetime.constantMax)/1.6f);

        // FIX: Send 'this' to the subscriber
        OnParticleSystemFinished?.Invoke(this);

        Destroy(gameObject);
    }
}