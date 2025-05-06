using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    
    public float walkSpeed = 15f;
    public float runSpeed = 30f;
    public float jumpPower = 10f;
    public float gravity = 9.81f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public float attackDamage = 10f;
    private float rotationX = 0;
    public float healthPlayer;
    public float deathZone = 100f;
    public GameObject playerArms;
    
 

    private bool canMove = true;
    private bool isGravityInverted = false;

    private Vector3 moveDirection = Vector3.zero;
    public CharacterController characterController;
    public AudioSource audioWalking;
    private Rigidbody playerRigidbody;
    public GameObject playerObject;
   
    public Camera playerCamera;
    public GameObject deathScreen;
    public GameObject mainUserInterface;
    public TextMeshProUGUI text;

    public GravityControl gravityControl;
    EnemyScript2 enemyScript2;
    GunSystem gunSystem;
    

    string debugText;
    private float mouseSensitivity = 2f;
    private void Awake()
    {
        
    }
    void Start()
    {
        healthPlayer = 100f;
        gunSystem = gunSystem.GetComponent<GunSystem>();

        characterController = GetComponent<CharacterController>();
    //    characterController.height = 1.5f;
      //  characterController.center = new Vector3(0, 0, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (canMove == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            MoveKeys();
        }

        print("walk speed" + walkSpeed);
        print("run speed " + runSpeed);
        HandleCameraLook();

        debugText = "";
        debugText += "\nthis is stuff";
        debugText += "\nMore stuff = " + isGravityInverted;
        text.SetText(" " + healthPlayer);

        if (Input.GetKeyDown(KeyCode.E) && canMove)
        {
            ToggleGravity();
        }

        if (Input.GetButtonDown("Jump") && canMove)
        {
            Debug.Log("Jump button pressed");
        }

        print("player health" + healthPlayer);
    }
    void HandleCameraLook()
    {
        if (!canMove || playerCamera == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

       
        transform.Rotate(0f, mouseX, 0f);

        
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localEulerAngles = new Vector3(rotationX, 0f, 0f);
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
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }

       
  
       
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
       
        playerArms.SetActive(false);
       
        mainUserInterface.SetActive(false);
        
        Destroy(mainUserInterface);
        


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
                moveDirection.y = isGravityInverted ? -jumpPower : jumpPower;
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
           
        }

        characterController.Move(moveDirection * Time.deltaTime);

       
    }


    

    void OnGUI()
    {
        GUI.Label(new Rect(10, 50, 200, 200), debugText);
    }
}
