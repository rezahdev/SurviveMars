using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert, canUnlock = true;

    [SerializeField]
    private int steps = 10;

    [SerializeField]
    private float sensitivity = 5f, 
                  weight = 0.4f, 
                  rollAngle = 10f,
                  rollSpeed = 3f;

    private float lookLimitMin = -70f;
    private float lookLimitMax = 80f;

    private int lastLookFrame;
    
    private Vector2 lookAngle;
    private Vector2 currentMouseLook;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        LockAndUnlockCursor();
        if(Cursor.lockState == CursorLockMode.Locked) {
            LookAround();
        }
    }
    void LockAndUnlockCursor() 
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(Cursor.lockState == CursorLockMode.Locked) 
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if(GlobalSettings.IsGamePlaying)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    void LookAround() 
    {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.Y), Input.GetAxis(MouseAxis.X));
        lookAngle.x += currentMouseLook.x * sensitivity * (invert ? 1f: -1f);
        lookAngle.y += currentMouseLook.y * sensitivity;
        lookAngle.x = Mathf.Clamp(lookAngle.x, lookLimitMin, lookLimitMax);

        lookRoot.localRotation = Quaternion.Euler(lookAngle.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngle.y, 0f);
    }
}
