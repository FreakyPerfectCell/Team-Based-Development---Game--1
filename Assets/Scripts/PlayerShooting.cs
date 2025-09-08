using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public Transform Aim;
    public GameObject bullet;
    public float fireForce = 10f;
    float shootCooldown = 0.25f;
    float shootTimer = 0.5f;

    void Update()
    {
       shootTimer += Time.deltaTime;

       if(Input.GetKeyDown(KeyCode.E))
       {
           OnShoot();
       }
    }

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
}
