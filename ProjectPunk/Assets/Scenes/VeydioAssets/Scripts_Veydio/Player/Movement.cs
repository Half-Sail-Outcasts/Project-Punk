using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;
    [SerializeField] Transform body;
    [SerializeField] float testSpeed; //remove after testing
    [SerializeField] float lookSpeed;
    [SerializeField] float xboxLookSpeed;
    [SerializeField] float addedJump = 1; //use this to add/adjust jump height via 'Class System'
    [SerializeField] float addedDoubleJump = 1;

    //private float turnSmooth; // THIRD PERSON
    float speed; //later add testSpeed value and keep private. *ALSO, possible variable for adjusting speed based on 'Class System'
    float crouchSpeed = 1;
    float sprintSpeed = 2;
    float slideSpeed = 5;
    float gravity;
    float mouseX, mouseY;
    float xClamp = 45f;
    float xRotation = 0f;

    //make private later
    public bool isJumping;
    public bool firstJumpDone;
    public bool canSecondJump;
    public bool isDoubleJump;
    public bool doneJumping;
    public bool clamberForward;
    public bool isCrouching;
    public bool isSprinting;
    public bool sprintButtonDown;
    private bool isFallSprint;
    public bool slideReady;
    public bool isSliding;
    public bool useXboxInput;

    Vector2 horizontalInput;
    Vector2 lookInput;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] ClamberCheck clamCheck;
    [SerializeField] Vector3 verticalVelocity;
    #endregion

    #region Enums
    IEnumerator AirTime()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        isJumping = false;
        firstJumpDone = true;
    }

    IEnumerator SecondAirTime()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        isDoubleJump = false;
        doneJumping = true;
        firstJumpDone = false;
    }

    IEnumerator ClamberTime()//still jittery FIXED > was character controller step offset 
    {
        yield return new WaitForSecondsRealtime(0.2f);
        clamberForward = true;
        gravity = 5;
        verticalVelocity.y += gravity * Time.deltaTime;
    }

    IEnumerator ClamberForwordTime()
    {
        yield return new WaitForSecondsRealtime(1f);
        clamberForward = false;
        //PGC.GetComponent<SphereCollider>().enabled = true;
    }

    IEnumerator SlideTime()
    {
        yield return new WaitForSecondsRealtime(0.5f); //add variable later if wanting to custom slide time/distance
        isSliding = false;
        slideReady = false;
    }

    IEnumerator ReadyForSlide()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        slideReady = true;
    }

    IEnumerator UndoSlideReadyStatus()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        slideReady = false;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        DirectionControl();
    }

    void DirectionControl()
    {
        #region moving and gravity (including jumping) variables

        //directional movements
        float hX = horizontalInput.x;
        float hY = horizontalInput.y;

        Vector3 direction = new Vector3(hX, 0, hY).normalized;
        lookInput = lookInput * Time.deltaTime;

        //jump movements
        if (isJumping)
        {
            //isSprinting = false;
            gravity = 30 + addedJump;
            StartCoroutine(AirTime());
        }

        if (!groundCheck.isGround && !isJumping && !doneJumping && firstJumpDone)
        {
            canSecondJump = true;
        }
        else
        {
            canSecondJump = false;
            isDoubleJump = false;
        }

        if (isDoubleJump)
        {
            gravity = 30 + addedDoubleJump;
            StartCoroutine(SecondAirTime());
            canSecondJump = false;
        }

        if (!isJumping && !isDoubleJump && !clamCheck.isClamber)
        {
            gravity = -9;

            if (groundCheck.isGround && !isJumping && !clamberForward && !clamCheck.isClamber)
            {
                verticalVelocity.y = -0.04f;

            }
            else if (!groundCheck.isGround && !isJumping || !groundCheck.isGround && !isDoubleJump || !groundCheck.isGround && !clamCheck.isClamber)
            {
                verticalVelocity.y += gravity * Time.deltaTime;
            }

        }
        else
        {
            verticalVelocity.y += gravity * Time.deltaTime;
        }

        if (groundCheck.isGround)
        {
            doneJumping = false;
            firstJumpDone = false;
            clamCheck.GetComponent<BoxCollider>().enabled = false;
            isFallSprint = false;
        }

        if (!groundCheck.isGround)
        {
            clamCheck.GetComponent<BoxCollider>().enabled = true;
        }

        if (isJumping || isDoubleJump || !groundCheck.isGround)
        {
            GetComponent<CharacterController>().stepOffset = 0;
        }
        else
        {
            GetComponent<CharacterController>().stepOffset = 0.3f;
        }


        controller.Move(verticalVelocity * Time.deltaTime);
        #endregion

        #region Sprint
        //sprintint behavior
        if (isSprinting && !isCrouching && !isSliding)
        {
            speed = sprintSpeed + testSpeed; //adding 'speed + #' causes it to add + # every frame. but adding 'testSpeed' is just fine.
                                             //enumorator for slideReady
            StartCoroutine(ReadyForSlide());

            if (isJumping || canSecondJump || isDoubleJump)
            {
                isFallSprint = true;
            }
        }
        else
        {
            speed = testSpeed;
            StartCoroutine(UndoSlideReadyStatus());
        }

        if (isFallSprint)
        {
            sprintSpeed = 0.5f;
            testSpeed = 4;
        }
        else
        {
            sprintSpeed = 2;
            testSpeed = 3;
        }
        #endregion
        #region Crouching
        //crouch behavior
        if (isCrouching && groundCheck.isGround && !isSliding)
        {
            body.localPosition = new Vector3(body.localPosition.x, -0.5f, body.localPosition.z);
            speed = crouchSpeed;

            if (isSprinting)
            {
                body.localPosition = new Vector3(body.localPosition.x, -0.5f, body.localPosition.z);
                speed = crouchSpeed;
            }
        }
        else
        {
            body.localPosition = new Vector3(body.localPosition.x, 0, body.localPosition.z);

            if (!isSprinting)
            {
                speed = testSpeed;
            }
        }
        #endregion
        #region Sliding
        //slide behavior
        if (isSprinting && isCrouching && groundCheck.isGround)
        {
            if (slideReady)
            {
                isSliding = true;
            }
        }

        if (isSliding)
        {
            body.localPosition = new Vector3(body.localPosition.x, -0.5f, body.localPosition.z);
            speed = slideSpeed;
            isSprinting = false;
            StartCoroutine(SlideTime());
        }
        #endregion
        #region Clambering
        //Clamber behavior
        if (clamCheck.isClamber)//Still jittery //disable collision
        {
            verticalVelocity.y = 5;

            groundCheck.isGround = false;
            controller.Move(transform.forward * 5 * Time.deltaTime);

            //PGC.GetComponent<SphereCollider>().enabled = false;
            //disable ground check while clamber
            //or start coroutine
            StartCoroutine(ClamberTime());
        }

        if (clamberForward)
        {

            controller.Move(transform.forward * 2 * Time.deltaTime);
            //start coroutine
            StartCoroutine(ClamberForwordTime());
        }
        #endregion

        //normal horizontal movements
        if (direction.magnitude >= 0.1f && !clamCheck.isClamber && !clamberForward)
        {
            Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
            controller.Move(horizontalVelocity * Time.deltaTime);

            if (horizontalInput.y <= 0 || isJumping || canSecondJump)
            {
                isSprinting = false;
            }

            if (horizontalInput.y > 0 && sprintButtonDown)
            {
                isSprinting = true;
            }
        }

        #region First Person Mouse and Axis controls
        //looking and aiming with mouse or right stick
        if (useXboxInput)
        {
            lookSpeed = xboxLookSpeed;
        }
        else
        {
            lookSpeed = 3;
        }

        transform.Rotate(Vector3.up, mouseX * lookSpeed * Time.deltaTime);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        cam.eulerAngles = targetRotation;
        #endregion

    }

    #region Calling From InputManager
    public void GetInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void GetLookInput(Vector2 _lookInput)
    {
        lookInput = _lookInput;

        mouseX = lookInput.x * 8;
        mouseY = lookInput.y * 0.5f;
    }

    public void JumpPressed()
    {
        if (/*PGC.isGround &&*/!doneJumping && !isCrouching && !canSecondJump && !isDoubleJump)
        {
            isJumping = true;
        }

        if (canSecondJump)
        {
            isDoubleJump = true;
            isJumping = false;
        }
    }

    public void JumpReleased()
    {
        isJumping = false;
    }

    public void CrouchPressed()
    {
        isCrouching = true;
    }

    public void CrouchReleased()
    {
        isCrouching = false;
    }

    public void SprintPressed()
    {
        if (horizontalInput.y > 0)
        {
            isSprinting = true;
        }

        sprintButtonDown = true;
    }

    public void SprintReleased()
    {
        isSprinting = false;

        sprintButtonDown = false;
    }

    #endregion
}
