using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;

    PlayerController controls;
    PlayerController.MovementActions movementActions;

    Vector2 horizontalInput;
    Vector2 lookInput;

    //Just for Testing
    [SerializeField] Text[] controlsText;
    [SerializeField] Text jumpCount;

    private void Awake()
    {
        controls = new PlayerController();
        movementActions = controls.Movement;

        //Just walking
        movementActions.HorizontalMovement.performed += context => horizontalInput = context.ReadValue<Vector2>();
        movementActions.LookTurn.performed += context => lookInput = context.ReadValue<Vector2>();

        //Sprinting speed
        movementActions.Sprint.performed += _ => movement.SprintPressed();
        movementActions.Sprint.canceled += _ => movement.SprintReleased();

        //Jumps
        movementActions.Jump.performed += _ => movement.JumpPressed();
        movementActions.Jump.canceled += _ => movement.JumpReleased();

        //Crouching down
        movementActions.Crouch.performed += _ => movement.CrouchPressed();
        movementActions.Crouch.canceled += _ => movement.CrouchReleased();


        //Hide Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            print(InputSystem.devices[i].description);
        }
    }

    private void Update()
    {
        movement.GetInput(horizontalInput);
        movement.GetLookInput(lookInput);

        for (int i = 0; i < InputSystem.devices.Count; i++)
        {
            if (InputSystem.devices[i].description.ToString().Contains("XInput"))
            {
                movement.useXboxInput = true;
            }
            else
            {
                movement.useXboxInput = false;
            }
        }

        DisplayControlScheme();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void DisplayControlScheme()//Just for testing purposes. Likely smarter way to do this if needed
    {
        if (movement.useXboxInput)
        {
            controlsText[0].text = "L - STICK";
            controlsText[1].text = "R - STICK";
            controlsText[2].text = "L - STICK(DOWN)";
            controlsText[3].text = "A BUTTON";
            controlsText[4].text = "B BUTTON";
        }
        else
        {
            controlsText[0].text = "W, A, S, D";
            controlsText[1].text = "MOUSE";
            controlsText[2].text = "L - SHIFT";
            controlsText[3].text = "SPACE";
            controlsText[4].text = "C";
        }

        if (movement.canSecondJump)
        {
            jumpCount.text = "1";
        }
        else if (movement.doneJumping)
        {
            jumpCount.text = "0";
        }
        else
        {
            jumpCount.text = "2";
        }
    }
}
