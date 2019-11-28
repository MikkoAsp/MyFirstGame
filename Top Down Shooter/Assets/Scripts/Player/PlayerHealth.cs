using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    PlayerMovement playerMovement;
    PlayerShoot playerShoot;
    AudioSource playerAudio;
    bool isDead;
    bool damaged;
    int healAmount = 20;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
        playerAudio = GetComponent<AudioSource>();
        playerShoot = GetComponentInChildren<PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
    public void TakeDamage(int amount)
    {
        playerAudio.Play();
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    void Death()
    {
        playerShoot.enabled = false;
        playerShoot.DisableEffects();
        isDead = true;
        playerMovement.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Medkit"))
        {
            currentHealth = currentHealth + healAmount;
            healthSlider.value = currentHealth;
            if(currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Medkit"))
        {
            Destroy(other.gameObject);
        }
    }
}
