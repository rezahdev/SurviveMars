using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModes : MonoBehaviour
{
    public float runSpeed = 10f;
    public float walkSpeed = 5f;
    public float crouchSpeed = 2f;

    private float normalHeight = 1.6f;
    private float crouchHeight = 1f;

    private bool isCrouching;

    private PlayerMovement playerMovement;
    private PlayerMovementAudio playerMovementAudio;
    private Transform lookRoot;

    private float runVol = 1f;
    private float crouchVol = 0.1f;
    private float walkVolMin = 0.2f;
    private float walkVolMax = 0.6f;

    private float walkStepDistance = 0.4f;
    private float runStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;

    private PlayerStats playerStats;
    private float sprintVal = 100f;
    private float sprintThreshold = 10f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);

        playerMovementAudio = GetComponentInChildren<PlayerMovementAudio>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        playerMovementAudio.volMin = walkVolMin;
        playerMovementAudio.volMax = walkVolMax;
        playerMovementAudio.stepDistance = walkStepDistance;
    }

    void Update()
    {
        Move();
    }

    void Move() 
    {
        if(sprintVal > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                playerMovement.speed = runSpeed;
                playerMovementAudio.stepDistance = runStepDistance;
                playerMovementAudio.volMin = runVol;
                playerMovementAudio.volMax = runVol;
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = walkSpeed;
            playerMovementAudio.volMin = walkVolMin;
            playerMovementAudio.volMax = walkVolMax;
            playerMovementAudio.stepDistance = walkStepDistance;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprintVal -= sprintThreshold * Time.deltaTime;
            if(sprintVal < 0f)
            {
                sprintVal = 0f;

                playerMovement.speed = walkSpeed;
                playerMovementAudio.volMin = walkVolMin;
                playerMovementAudio.volMax = walkVolMax;
                playerMovementAudio.stepDistance = walkStepDistance;
            }
            playerStats.DisplayStaminaStat(sprintVal);
        }
        else
        {
            if(sprintVal != 100f)
            {
                sprintVal += (sprintThreshold / 2f) * Time.deltaTime;
                playerStats.DisplayStaminaStat(sprintVal);
            }

            if (sprintVal > 100f)
            {
                sprintVal = 100f;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(isCrouching) 
            {
                lookRoot.localPosition = new Vector3(0f, normalHeight, 0f);
                playerMovement.speed = walkSpeed;

                playerMovementAudio.volMin = walkVolMin;
                playerMovementAudio.volMax = walkVolMax;
                playerMovementAudio.stepDistance = walkStepDistance;
            }
            else 
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;

                playerMovementAudio.stepDistance = crouchStepDistance;
                playerMovementAudio.volMin = crouchVol;
                playerMovementAudio.volMax = crouchVol;

            }
            isCrouching = !isCrouching;
        }
    }
}
