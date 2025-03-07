using UnityEngine;

public class GravityScript : MonoBehaviour
{
    // This function inverts the gravity on the start
    void Start()
    {
        // Get the current gravity direction
        Vector3 currentGravity = Physics.gravity;

        // Invert the gravity (make it point in the opposite direction)
        Physics.gravity = -currentGravity;
    }
}
