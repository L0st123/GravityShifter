using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    public CharacterController controller;

    public float gravity = 9.81f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f; // Speed of rotation
    private Vector3 velocity;
    private bool isFlipped = false;
    private bool flipQueued = false;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        HandleMovement();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G Pressed");
            flipQueued = true;
        }

        // Smoothly rotate the player towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (flipQueued)
        {
            FlipGravity();
            flipQueued = false;
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
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

    void FlipGravity()
    {
        isFlipped = !isFlipped;
        velocity.y = 0f;

        
        targetRotation *= Quaternion.Euler(0, 0, -180);

        
        GravityControl gc = GetComponent<GravityControl>();
        if (gc != null)
        {
            gc.isGravityInverted = isFlipped;
        }

        Debug.Log("Gravity Flipped");
    }
}
