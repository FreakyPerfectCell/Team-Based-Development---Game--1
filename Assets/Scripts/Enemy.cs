using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float _speed;
    private Rigidbody2D rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction = new Vector2(Mathf.Sign(direction.x), 0);
        }
        else
        {
            direction = new Vector2(0, Mathf.Sign(direction.y));
        }

        rb.MovePosition(rb.position + direction * _speed * Time.deltaTime);
    }
}
