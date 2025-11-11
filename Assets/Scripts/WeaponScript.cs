using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    public float weaponDamage = 10.0f;
    public float weaponRange = 50.0f;
    public float fireRate = 10.0f;
    public float nextFire = 0f;
    public Camera fpCamera;
    
    public void Shoot()
    {
        RaycastHit hit;
        //Debug.Log($"haha");
        Debug.DrawRay(transform.position, transform.forward * weaponRange, Color.green);

        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, weaponRange))
        {
            //Debug.Log(hit.collider.name);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                //Apply damage to enemy
                Debug.Log(hit.collider.name);
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(weaponDamage);
            }
        }
    }

    public void FireShot(InputAction.CallbackContext ctx)
    {
        //Debug.Log($"Fireshot");
        if (ctx.performed && Time.time >= nextFire)
        {
            nextFire = Time.time + 1.0f / fireRate;
            Shoot();
        }
    }
}
