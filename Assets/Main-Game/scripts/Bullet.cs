using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            EnemyScript2 enemy = collision.gameObject.GetComponent<EnemyScript2>();
            if (enemy != null)
            {
               //   enemy.TakeDamage(damage); 
            }
        }

        Destroy(gameObject); 
    }
}
