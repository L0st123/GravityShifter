using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript playerScript = other.GetComponentInParent<PlayerScript>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(playerScript.deathZone);
            }
            else
            {
                Debug.LogWarning("PlayerScript not found on object tagged 'Player'.");
            }
        }
    }
}
