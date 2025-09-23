using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    // transform is where the bullet comes out of 
    // gameobject slots for both bullets
    // both visible and worked on in the inspector
    public Transform Aim;
    public GameObject bullet;
    public GameObject chargeBullet;

    [Header("Standard Shot")]
    public float fireForce = 10f;
    float shootCooldown = 0.25f;
    private float shootTimer = 0.5f;

    [Header("Charged Shot")]
    public float minChargeTime = 0.4f;
    public float ChargeTime = 0f;



    void Update()
    {
       shootTimer += Time.deltaTime;

       // if shoot is pressed it'll call the shoot function
       if(Input.GetKeyDown(KeyCode.E))
       {
           OnShoot();
       }

       // holding shoot starts the timer of charge
       if(Input.GetKey(KeyCode.E))
       {
           ChargeTime += Time.deltaTime;
       }

       // letting go of shoot calls the charge shot funciton
       if(Input.GetKeyUp(KeyCode.E))
       {
           OnChargeShoot();
       }
    }

    // resets the timer first then fires the prefab and does the math idk I followed a tutorial
    void OnShoot()
    {
        if(shootTimer > shootCooldown)
        {
            shootTimer = 0;
            GameObject intBullet = Instantiate(bullet, Aim.position, Aim.rotation);
            intBullet.GetComponent<Rigidbody2D>().AddForce(-Aim.up * fireForce, ForceMode2D.Impulse);
            Destroy(intBullet, 2f);
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
            Destroy(intChargeBullet, 2f);
        }
    }
}
