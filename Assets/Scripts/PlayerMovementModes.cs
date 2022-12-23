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
    private Transform lookRoot;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
    }

    void Update()
    {
        Move();
    }

    void Move() 
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching) 
        {
            playerMovement.speed = runSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = walkSpeed;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(isCrouching) 
            {
                lookRoot.localPosition = new Vector3(0f, normalHeight, 0f);
                playerMovement.speed = walkSpeed;
            }
            else 
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;
            }
            isCrouching = !isCrouching;
        }
    }
}
