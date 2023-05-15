using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all input and is used to then connect and read those inputs
/// </summary>
public class InputHandler : MonoBehaviour
{
    private static InputHandler singletonInstance;
    public static InputHandler instance
    {
        get
        {
            return singletonInstance;
        }
    }

    [HideInInspector]
    public Vector3 MoveDirection = Vector3.forward;

    public Action<Vector2> MouseInput;

    public Action<Vector2> MoveInput;

    public Action Jump;

    public Action<bool> Slide;

    public Action Interact;



    private void Awake()
    {
        if (singletonInstance == null)
        {
            singletonInstance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of the singleton 'InputHandler' exist.");
            Destroy(this);
        }

        LockMouse(true);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector2 mouseMovement = new Vector2(mouseX, mouseY).normalized;

        if (mouseMovement != Vector2.zero)
        {
            MouseInput?.Invoke(mouseMovement);
        }

        Vector2 inputMovement = Vector2.zero;

        inputMovement += Vector2.up * Convert.ToInt32(Input.GetKey(KeyCode.W));
        inputMovement += Vector2.down * Convert.ToInt32(Input.GetKey(KeyCode.S));
        inputMovement += Vector2.left * Convert.ToInt32(Input.GetKey(KeyCode.A));
        inputMovement += Vector2.right * Convert.ToInt32(Input.GetKey(KeyCode.D));

        inputMovement = inputMovement.normalized;

        if (inputMovement != Vector2.zero)
        {
            MoveInput?.Invoke(inputMovement);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact?.Invoke();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Slide?.Invoke(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Slide?.Invoke(false);
        }
    }

    public void LockMouse(bool isMouseLocked)
    {
        Cursor.visible = !isMouseLocked;
        Cursor.lockState =(isMouseLocked)? CursorLockMode.Locked : CursorLockMode.None;
    }
}
