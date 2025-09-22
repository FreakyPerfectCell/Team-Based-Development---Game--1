using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public static Enemy instance;

    [Header("Health Crap")]
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth = 3;

    [Header("Movement Crap")]
    [SerializeField] private float _speed;
    private Rigidbody2D rb;
    private Transform player;

    [Header("Damage Crap")]
    public int damageAmount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Player.instance.TakeDamage(damageAmount);
        }
    }


    public void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
