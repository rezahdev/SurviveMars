using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip screamClip, dieClip;

    [SerializeField]
    private AudioClip[] attackClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    } 
    public void PlayScreamSound()
    {
        audioSource.clip = screamClip;
        audioSource.Play();
    }
    public void PlayAttackSound()
    {
        audioSource.clip = attackClip[Random.Range(0, attackClip.Length)];
        audioSource.Play();
    }
    public void PlayDeathSound()
    {
        audioSource.clip = dieClip;
        audioSource.Play();
    }
}
