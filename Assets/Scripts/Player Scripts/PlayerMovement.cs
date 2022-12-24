using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;

    public float speed = 5f;
    public float jumpForce = 10f;
    private float gravity = 20f;
    private float verticalVelocity = 0f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer() 
    {
        direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        direction = transform.TransformDirection(direction);
        direction *= speed * Time.deltaTime;

        ApplyGravity();
        characterController.Move(direction);
    }

    void ApplyGravity() 
    {
        verticalVelocity  -= gravity * Time.deltaTime;
        PlayerJump();
        direction.y = verticalVelocity;
    }

    void PlayerJump() 
    {
        if(characterController.isGrounded  && Input.GetKeyDown(KeyCode.Space)) 
        {
            verticalVelocity = jumpForce;
        }
    }
}