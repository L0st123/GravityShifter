using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    public GravityControl gravityControl;
    public Camera playerCamera;
    public float walkSpeed = 15f;
    public float runSpeed = 30f;
    public float jumpPower = 10f;
    public float gravity = 9.81f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    public int speed = 2;
    public AudioSource audioWalking;
    private Rigidbody playerRigidbody;
    public GameObject playerObject;
    public float healthPlayer;
    private bool canMove = true;
    private bool isGravityInverted = false;
    public GameObject deathScreen;
    int healthLeft; 
    public TextMeshProUGUI text;
 

    string debugText;

    private void Awake()
    {
        healthPlayer = healthLeft;
    }
    void Start()
    {
        healthPlayer = 100f;
       

        characterController = GetComponent<CharacterController>();
    //    characterController.height = 1.5f;
      //  characterController.center = new Vector3(0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        debugText = "";

        debugText += "\nthis is stuff";
        debugText += "\nMore stuff = " + isGravityInverted;
        text.SetText(healthLeft + "|");

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleGravity();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed");
        }
       
        //print("player health"+ healthPlayer);
        MoveKeys();
    }

    void ToggleGravity()
    {
        isGravityInverted = !isGravityInverted;
        transform.Rotate(180, 0, 0);
        characterController.center = new Vector3(0, isGravityInverted ? -0f : 0f, 0);
        Physics.SyncTransforms();
    }


    public void TakeDamage(float damage)
    {
        healthPlayer -= damage;
        if (healthPlayer <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathScreen != null) deathScreen.SetActive(true);
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }


    void MoveKeys()
    {
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        print("Grounded=" + gravityControl.IsGrounded());

        // Jumping
        if (gravityControl.IsGrounded())
        {
            if (Input.GetButtonDown("Jump") && canMove)
            {
                Debug.Log("Jump button pressed");
                //moveDirection.y = isGravityInverted ? -jumpPower : jumpPower;
                gravityControl.DoJump();
            }
            else
            {
                //moveDirection.y = (isGravityInverted ? gravity : -gravity) * Time.deltaTime;
            }
        }
        

        // Crouch
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


    

    void OnGUI()
    {
        GUI.Label(new Rect(10, 50, 200, 200), debugText);
    }
}
