using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public CharacterController controller;

    public float moveSpeed = 5f;
    public float floatSpeed = 10f;
    private Vector3 velocity;
    private bool isFloating = false;

    void Update()
    {
        HandleMovement();
        ToggleFloat();

        if (isFloating)
        {
            ApplyFloatUpward();
        }
        else
        {
            ApplyNormalGravity();
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void ToggleFloat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isFloating = !isFloating;
            velocity.y = 0;
        }
    }

    void ApplyFloatUpward()
    {
        velocity.y = floatSpeed;
        controller.Move(velocity * Time.deltaTime);
    }

    void ApplyNormalGravity()
    {
        velocity.y = -9.81f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocity.y = 0;
        }
    }
}
