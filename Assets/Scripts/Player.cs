using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float maxSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveDirection;

    [SerializeField] public Animator anim;
    private string lastDirection = "Down";

    public Transform Aim;
    bool isWalking = false;

    [Header("UI Crap")]
    public int currentHealth;
    public int maxHealth;
    public GameObject deadScreen;
    private bool hasDied;
    public TextMeshProUGUI healthText;
    // makes it so we can use instance
    public static Player instance;

    // instance so we can use this with other scripts
    private void Awake()
    {
        instance = this;
        // instance this means this script
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        moveSpeed = maxSpeed;
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        CheckPoisonTile();
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        if (anim == null) return;

        string animationName = "";

        if (moveDirection == Vector2.zero)
            animationName = "Idle";
        else
            animationName = "Walking";

        anim.Play(animationName + lastDirection);
    }

    // this is gonna be alot for HandleAnimations()
    // if we arent moving we play an idle anim for whatever the lastDirection was pressed
    // if we went up last then we play IdleUp
    // we dont play each individual animation, its always gonna check for the idle and if were not idle
    // we will set animationName to walking then pair it with the lastDirection
    // it seems confusing but look at it again its fairly simple
    // just make sure that the animation clips themselves are named like "WalkingRight" or "IdleUp"

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;         
        if(!hasDied)
        {
            if (isWalking)
                {
                    //rotates the aim transform
                    Vector3 vector3 = Vector3.left * moveInput.x + Vector3.down * moveInput.y;
                    Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
                }
        }
    }

    private Vector3 GetDirection(Vector3 input)
    {
        //one for each direction
        //needed for specifically 4 way movement
        //lastDirection used for the animations
        //vectors to update move the aim
        //finalDirection is like the current direction also for animations

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
        //checks which direction you press and gets the direction for the other parts of the code
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



    void CheckPoisonTile()
    {
        GameObject poisonTilemapGO = GameObject.FindGameObjectWithTag("PoisonSpreadable");
        if (poisonTilemapGO != null)
        {
            //checks with PoisonSmoke and checks the tile the player is standing on
            var manager = poisonTilemapGO.GetComponent<PoisonSmoke>();
            manager.OnPlayerStepped(transform.position, gameObject, gameObject);
        }
    }

    // maunally sets the speed down 10% per level
    // easier and prevents the speed from going down to 0
    // plus it works
    public void SlowMove1()
    {
        // 10%
        moveSpeed = 4.5f;
    }

    public void SlowMove2()
    {
        // 20%
        moveSpeed = 4.0f;
    }

    public void SlowMove3()
    {
        // 30%
        moveSpeed = 3.5f;
    }

    public void SlowMove4()
    {
        // 40%
        moveSpeed = 3.0f;
    }

    public void SlowMove5()
    {
        // 50%
        moveSpeed = 2.5f;
    }

    public void SlowMove0()
    {
        // 0%
        moveSpeed = maxSpeed;
    }
}
