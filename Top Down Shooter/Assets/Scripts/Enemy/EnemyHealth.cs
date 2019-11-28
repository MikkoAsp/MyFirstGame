using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioSource enemyAudio;
    BoxCollider boxCollider;
    ParticleSystem hitParticles;
    bool isDead;
    bool isSinking;
    // Start is called before the first frame update
    void Awake()
    {
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        if (currentHealth <= 0)
        {
            Death();
            StartSinking();
        }
    }
    void Death()
    {
        isDead = true;
        boxCollider.isTrigger = true;
    }
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
