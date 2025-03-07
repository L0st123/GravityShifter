using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 7f;
    public float runSpeed = 10f;
    public float jumpPower = 7f;
    public float gravity = 9.81f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    public Animator animator;
    public int speed = 2;
    public AudioSource audioWalking;
    private Rigidbody playerRigidbody;
    public GameObject playerObject; 

    private bool canMove = true;

    // Add a flag to check if gravity is inverted
    private bool isGravityInverted = false;

    void Start()
    {
        
       animator.SetBool("run", false);
        animator.SetBool("walk", true);

        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for user input to toggle gravity inversion
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleGravity();
        }

        MoveKeys();
    }

    // Function to toggle gravity inversion
    void ToggleGravity()
    {
        isGravityInverted = !isGravityInverted;

        // Invert the gravity in the scene
        Physics.gravity = isGravityInverted ? new Vector3(0, -gravity, 0) : new Vector3(0, gravity, 0);
        transform.Rotate(180, 0, 0);
        Physics.SyncTransforms();
    }

    void MoveKeys()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (Input.GetKeyDown(KeyCode.W)) 
        { animator.SetBool("walk", true); }
        else
        {
            animator.SetBool("walk", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        { animator.SetBool("run", true); }
        else
        {
            animator.SetBool("run", false);
        }



        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime * (isGravityInverted ? -1 : 1); 
        }

        if (Input.GetKey(KeyCode.LeftControl) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 8f;
            runSpeed = 10f;
        }

       
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
