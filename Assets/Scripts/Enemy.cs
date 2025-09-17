using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Health Crap")]
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;



    [Header("Movement Crap")]
    [SerializeField] private float _speed;
    private Rigidbody2D rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
