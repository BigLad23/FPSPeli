using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
    public float damage = 100f;
    public float range = 100f;
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public ParticleSystem MuzzleFlash;
    public Camera playerCamera;
    public Animator animator;


    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable ()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading)
            return;
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo) // If the player presses R reload begins
        {
            Debug.Log("test");
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Debug.Log(currentAmmo);
        currentAmmo = currentAmmo - 1;
        MuzzleFlash.Play(); // plays the muzzle flash animation when you fire
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null) // checks if you hit a enemy
            {
                enemy.TakeDamage(damage);
            }
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        currentAmmo = maxAmmo; // Sets the current ammo to max ammo
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(reloadTime - .25f);
        isReloading = false;
    }
}
