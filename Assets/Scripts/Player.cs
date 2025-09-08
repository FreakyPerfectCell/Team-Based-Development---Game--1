using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveDirection;

    [SerializeField] public Animator anim;
    private string lastDirection = "Down";

    public Transform Aim;
    bool isWalking = false;

    public int currentHealth;
    public int maxHealth;
    public GameObject deadScreen;
    private bool hasDied;
    public TextMeshProUGUI healthText;
    public static Player instance;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        //HandleAnimations();
    }

    //private void HandleAnimations()
    //{
        //if (anim == null) return;

        //string animationName = "";

        //if (moveDirection == Vector2.zero)
            //animationName = "Idle";
        //else
            //animationName = "Walking";

        //anim.play(animationName + lastDirection);
    //}

    private void FixedUpdate()
    {

        rb.linearVelocity = moveDirection * moveSpeed;         

        if(!hasDied)
        {
            if (isWalking)
                {
                    Vector3 vector3 = Vector3.left * moveInput.x + Vector3.down * moveInput.y;
                    Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
                }
        }
    }

    private Vector3 GetDirection(Vector3 input)
    {
        Vector3 finalDirection = Vector3.zero;
        if (input.y > 0.01f)
        {
            lastDirection = "Up";
            finalDirection = new Vector2(0, 1);
            isWalking = true;
            Vector3 vector3 = Vector3.left * finalDirection.x + Vector3.down * finalDirection.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        else if (input.y < -0.01f)
        {
            lastDirection = "Down";
            finalDirection = new Vector2(0, -1);
            isWalking = true;
            Vector3 vector3 = Vector3.left * finalDirection.x + Vector3.down * finalDirection.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        else if (input.x > 0.01f)
        {
            lastDirection = "Right";
            finalDirection = new Vector2(1, 0);
            isWalking = true;
            Vector3 vector3 = Vector3.left * finalDirection.x + Vector3.down * finalDirection.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        else if (input.x < -0.01)
        {
            lastDirection = "Left";
            finalDirection = new Vector2(-1, 0);
            isWalking = true;
            Vector3 vector3 = Vector3.left * finalDirection.x + Vector3.down * finalDirection.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
        else
            finalDirection = Vector2.zero;
            isWalking = false;

        return finalDirection;
    }

    private void OnMove(InputValue value)
    {   
        moveInput = value.Get<Vector2>().normalized;
        moveDirection = GetDirection(moveInput);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            deadScreen.SetActive(true);
            hasDied = true;
            currentHealth = 0;
        }

        healthText.text = currentHealth.ToString();
    }
}
