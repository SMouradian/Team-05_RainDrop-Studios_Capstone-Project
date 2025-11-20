using UnityEngine;
using UnityEngine.AI;
using MoreMountains.Feedbacks;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth = 100;
    public MMFeedbacks CameraShake;
    public ParticleSystem PS;
    public MMFeedbacks Barfx;

    [Header("Physics / AI")]
    private Rigidbody rb;
    private HostileAI hostileAI;
    private NavMeshAgent navMeshAgent;
    private bool isDead = false;
    public float flyForce = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hostileAI = GetComponent<HostileAI>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        //Updates the health bar at start
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        else
        {
            Debug.LogWarning("FloatingHealthBar component not found in children.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);


        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Update the health bar
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            Barfx.PlayFeedbacks();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0 && !isDead)
        {
            isDead = true;
            PS.Play();
            CameraShake.PlayFeedbacks();
            hostileAI.enabled = false;
            navMeshAgent.enabled = false;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce((-transform.forward + Vector3.up) * flyForce, ForceMode.Impulse);
            rb.AddTorque(Vector3.right * 10f, ForceMode.Impulse);
            Invoke("StopCameraShake", 0.3f);
            Invoke("DestroyEnemy", 10f);
        }
    }

    void StopCameraShake()
    {
        CameraShake.StopFeedbacks();
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

