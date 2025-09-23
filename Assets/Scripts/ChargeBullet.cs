using UnityEngine;

public class ChargeBullet : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Enemy enemy = other.GetComponent<Enemy>();
			if (enemy != null)
			{
				enemy.TakeCharge();
			}
			Destroy(gameObject);
		}
	}
}

// when we collide with a object with tag "Enemy" we call TakeCharge() | see enemy script for more
// after collision we destroy the bullet object
