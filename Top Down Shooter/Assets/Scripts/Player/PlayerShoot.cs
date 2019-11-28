using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int damagePerShot;
    public float timeBetweenShots = 0.15f;
    public float range = 100f;
    public int ammo = 30;
    public float reloadTime = 3f;
    public float damageBoostTime;
    bool isReloading;
    bool hasPowerUp;

    public Transform shellSpawnPoint;
    public GameObject shell;
    AudioManager audioManager;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem muzzleFlash;
    LineRenderer gunLine;
    Light gunLight;

    float effectsDisplayTime = 0.2f;


    private void Awake()
    {
        muzzleFlash = GetComponent<ParticleSystem>();
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.ammo = ammo;
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R) && ammo < 30)
        {
            isReloading = true;
            audioManager.PlayAudioClip("reload");
            Invoke(("Reload"), reloadTime);
        }

        if (Input.GetButton("Fire1") && timer >= timeBetweenShots && ammo > 0 && !isReloading)
        {
            Shoot();
            ammo--;
        }

        if(timer >= timeBetweenShots * effectsDisplayTime)
        {
            DisableEffects();
        }
    }
    void Reload()
    {
        ammo = 30;
        isReloading = false;
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    void Shoot()
    {
        Instantiate(shell, shellSpawnPoint.position, shellSpawnPoint.rotation);
        //Määrittelee luodin vahingon määrän, jos ei ole lisädamagea päällä
        if (!hasPowerUp)
        {
            damagePerShot = Random.Range(17, 25);
        }

        timer = 0f;

        audioManager.PlayAudioClip("shootSound");

        gunLight.enabled = true;

        muzzleFlash.Stop();
        muzzleFlash.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //Vie tassa damagen ja osumakohdan enemyhealtille
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boost"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            damagePerShot = damagePerShot + damagePerShot;
            StartCoroutine(bulletDamageBoost());
        }
    }
    IEnumerator bulletDamageBoost()
    {
        yield return new WaitForSeconds(damageBoostTime);
        hasPowerUp = false;
    }
}
