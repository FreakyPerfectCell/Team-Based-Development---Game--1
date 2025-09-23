using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}

// when we collide with a object with tag "Enemy" we call TakeDamage() | see enemy script for more
// after collision we destroy the bullet object
