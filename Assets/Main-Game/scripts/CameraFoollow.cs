using UnityEngine;

public class CameraFoollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 1.5f, -5f);
    public float smoothSpeed = 0.125f;

    void Update()
    {
        // Smoothly move the camera to the player's position
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Always look at the player
        transform.LookAt(player);
    }
}
