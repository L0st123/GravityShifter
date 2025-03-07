using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile to shoot
    public Transform shootingPoint;     // The point from which the projectile is shot
    public float shootForce = 20f;      // The force applied to the projectile
    public float fireRate = 0.5f;       // Time between shots (rate of fire)
    private float nextFireTime = 0f;    // Time until the next shot can be fired

    void Update()
    {
        // Check for firing input (left mouse button or any other input)
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + fireRate; // Set the next time the gun can shoot
        }
    }

    void ShootProjectile()
    {
        // Instantiate the projectile at the shooting point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);

        // Get the Rigidbody component of the projectile to apply force
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Apply force to the projectile in the forward direction of the shooting point (gun barrel)
        if (rb != null)
        {
            rb.AddForce(shootingPoint.forward * shootForce, ForceMode.VelocityChange);
        }
    }
}
