using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public ParticleSystem MuzzleFlash;
    public Camera playerCamera;
    public Animator animator;
    public GameObject impactEffect;
    private float nextShot = 0f;
    public Text ammoDisplay;


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
        ammoDisplay.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo) // If the player presses R reload begins
        {
            StartCoroutine(Reload());
            ammoDisplay.text = "Reloading...";
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextShot)
        {
            nextShot = Time.time + 1f/fireRate;
            Shoot();
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            ammoDisplay.text = "Reloading...";
            return;
        }
    }
    void Shoot()
    {
        currentAmmo = currentAmmo - 1;
        Debug.Log(currentAmmo);
        MuzzleFlash.Play(); // plays the muzzle flash animation when you fire
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            EnemyMovement enemy = hit.transform.GetComponent<EnemyMovement>();

            if (enemy != null) // checks if you hit a enemy
            {
                enemy.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 100f);
            }
           GameObject Impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
           Destroy(Impact, 2f);
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
        Debug.Log("Reloading Done!");
        isReloading = false;
    }
}
