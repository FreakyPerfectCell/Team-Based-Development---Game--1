using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // makes it so we can use instance
    // instance this means this script
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
    // no value written value means we automatically go to 1

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    // damages player on collision
    // instance calls player script and takes does damageAmount - playerHealth
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Player.instance.TakeDamage(damageAmount);
        }
    }

    // takes regular shot damage 
    // currentHealth--; takes away one health, i didnt know how to do multiple in one line ._.
    // takes away 2 health
    // last lines are logic at 0 or below 0 health destroy enemy gameObject
    public void TakeDamage()
    {
        currentHealth--;
        currentHealth--;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // takes charge shot damage
    // takes away 4 health
    public void TakeCharge()
    {
        currentHealth--;
        currentHealth--;
        currentHealth--;
        currentHealth--;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
