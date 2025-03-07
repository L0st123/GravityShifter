using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    public CharacterController controller;

    public float gravity = 9.81f;
    public float moveSpeed = 5f;
    private Vector3 velocity;
    private bool isFlipped = false;

    void Update()
    {
        HandleMovement();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.G))
        {
            FlipGravity();
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
        velocity += (isFlipped ? Vector3.up : Vector3.down) * gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocity = Vector3.zero;
        }
    }

    void FlipGravity()
    {
        isFlipped = !isFlipped;
        transform.Rotate(180, 0, 0);
     
        velocity = Vector3.zero;
    }
}

