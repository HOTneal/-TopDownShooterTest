using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffects : MonoBehaviour
{
    [SerializeField] private AudioClip m_SoundGrenade;
        
    private AudioSource m_AudioSource;
    private ParticleSystem m_ParticleSystem;

    private IEnumerator Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.PlayOneShot(m_SoundGrenade);
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
        Destroy(gameObject);
    }
}
