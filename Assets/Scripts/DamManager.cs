using UnityEngine;

public class DamManager : MonoBehaviour
{

    [Header("Health Crap")]
    public int currentHealth;
    public int maxHealth;

    public static DamManager instance;
    private Rigidbody2D rb;

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void CheckPoisonTile()
    {
        GameObject poisonTilemapGO = GameObject.FindGameObjectWithTag("PoisonSpreadable");
        if (poisonTilemapGO != null)
        {
            var manager = poisonTilemapGO.GetComponent<PoisonSmoke>();
            manager.OnPlayerStepped(transform.position, gameObject, gameObject);
        }
    }
}
