using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public CharacterController controller;

    public float moveSpeed = 5f;
    public float floatSpeed = 10f;
    public float gravity = 9.81f;
    private Vector3 velocity;
    private bool isFloating = false;
    public bool isFlipped = false;
    public bool isGravityInverted = false;
    public float jumpPower = 10f;
    public float distanceToGround = 1.2f;


    private Vector3 moveDirection;

    void Update()
    {
        HandleMovement();
        ToggleFloat();
        HandleGravityFlip();

       
        if (isFloating)
        {
            ApplyFloatUpward();
        }
        else
        {
            ApplyGravity();
        }
    }

    void HandleMovement()
    {
        
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;

      
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

     void ToggleFloat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isFloating = !isFloating;
            velocity.y = 0; 
        }
    }

    void HandleGravityFlip()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isFlipped = !isFlipped;
            velocity.y = 0f;  
            transform.Rotate(0,180,0);    
        }
    }

    void ApplyFloatUpward()
    {
        velocity.y = floatSpeed;  
        controller.Move(velocity * Time.deltaTime);
    }

    void ApplyGravity()
    {
        velocity.y += (isFlipped ? gravity : -gravity) * Time.deltaTime;  
        controller.Move(velocity * Time.deltaTime);
       

        if (controller.isGrounded)
        {
            velocity.y = 0f;  
        }
    }
    public bool IsGrounded()
    {
        Vector3 origin = transform.position;
        Vector3 direction = isGravityInverted ? Vector3.up : Vector3.down;
        return Physics.Raycast(origin, direction, distanceToGround + 0.1f);
    }


    public void DoJump()
    {
        //velocity.y = isGravityInverted ? jumpPower : -jumpPower;

        if (isGravityInverted)
        {
            velocity.y = -jumpPower;
        }
        else
        {
            velocity.y = jumpPower;
        }

    }
}
