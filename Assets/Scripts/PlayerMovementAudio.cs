using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAudio : MonoBehaviour
{
    private AudioSource footstepSound;

    [SerializeField]
    private AudioClip[] footstepClip;

    private CharacterController characterController;

    [HideInInspector]
    public float volMin, volMax;

    private float accumulatedDistance;

    [HideInInspector]
    public float stepDistance;

    void Awake()
    {
        footstepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        PlaySound();
    }

    void PlaySound() 
    {
        if(!characterController.isGrounded)
        {
            return;
        }

        if(characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;

            if(accumulatedDistance > stepDistance)
            {
                footstepSound.volume = Random.Range(volMin, volMax);
                footstepSound.clip = footstepClip[Random.Range(0, footstepClip.Length)];
                footstepSound.Play();

                accumulatedDistance = 0f;
            }
        }
    }
}
