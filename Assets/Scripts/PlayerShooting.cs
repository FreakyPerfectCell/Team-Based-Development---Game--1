using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    // transform is where the bullet comes out of 
    // gameobject slots for both bullets
    // both visible and worked on in the inspector
    [Header("Shooter Crap")]
    public Transform Aim;
    public GameObject bullet;
    public GameObject chargeBullet;
    public GameObject dam;

    [Header("Standard Shot")]
    public float fireForce = 10f;
    float shootCooldown = 0.25f;
    private float shootTimer = 0.5f;

    [Header("Charged Shot")]
    public float minChargeTime = 0.4f;
    public float ChargeTime = 0f;

    private float spacePressedTime = 0f;
    public float spacePressed = 0f;
    // how fast the second press must be to work
    [SerializeField] private float tapMaxDelay = 0.3f;

    void Update()
    {
       shootTimer += Time.deltaTime;

       // if shoot is pressed it'll call the shoot function
       if(Input.GetKeyDown(KeyCode.Space))
       {
           OnShoot();
       }

       // holding shoot starts the timer of charge
       if(Input.GetKey(KeyCode.Space))
       {
           ChargeTime += Time.deltaTime;
       }

       // letting go of shoot calls the charge shot funciton
       if(Input.GetKeyUp(KeyCode.Space))
       {
           OnChargeShoot();
       }

       if(Input.GetKeyDown(KeyCode.LeftShift))
       {
           if (spacePressed == 0)
           {
               spacePressed = 1;
               spacePressedTime = Time.deltaTime;
           }
           else if (spacePressed == 1 && spacePressedTime <= tapMaxDelay)
           {
               Dam();
               spacePressed = 0;
           }
           else
           {
               spacePressed =  0;
               spacePressedTime = Time.deltaTime;
           }

           if (spacePressedTime == 1 && Time.deltaTime > tapMaxDelay)
           {
               spacePressed = 0;
           }
       }
    }

    // resets the timer first then fires the prefab and does the math idk I followed a tutorial
    void OnShoot()
    {
        if(shootTimer > shootCooldown)
        {
            ChargeTime = 0; // prevents charge shot from shooting when spamming shoot
            shootTimer = 0;
            GameObject intBullet = Instantiate(bullet, Aim.position, Aim.rotation);
            intBullet.GetComponent<Rigidbody2D>().AddForce(-Aim.up * fireForce, ForceMode2D.Impulse);
            Destroy(intBullet, .3f); // destroys after .3 seconds or 3 tiles
        }
    }

    // resets the timer first then fires the prefab and does the math idk I followed a tutorial
    void OnChargeShoot()
    {
        if(ChargeTime >= minChargeTime)
        {
            ChargeTime = 0;
            GameObject intChargeBullet = Instantiate(chargeBullet, Aim.position, Aim.rotation);
            intChargeBullet.GetComponent<Rigidbody2D>().AddForce(-Aim.up * fireForce, ForceMode2D.Impulse);
            Destroy(intChargeBullet, .5f); // destroys after .5 seconds or 5 tiles
        }
    }

    void Dam()
    {
        Vector3 spawnPos = Aim.position + Aim.up * -1f;
        GameObject intDam = Instantiate(dam, spawnPos, Aim.rotation);
    }
}
