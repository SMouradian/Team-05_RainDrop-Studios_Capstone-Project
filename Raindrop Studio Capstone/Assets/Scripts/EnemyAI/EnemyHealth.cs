using UnityEngine;
using UnityEngine.AI;
using MoreMountains.Feedbacks;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;
    private FloatingHealthBar healthBar;
    
    [Header("Effects")]
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
        currentHealth = maxHealth;
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


        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            if (Barfx != null)
            {
                Barfx.PlayFeedbacks();
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (healthBar != null)
        {
            healthBar.FadeOutAndDestroy();
        }

        if (PS != null)
        {
            PS.Play();
        }

        if (CameraShake != null)
        {
            CameraShake.PlayFeedbacks();
            Invoke(nameof(StopCameraShake), 0.3f);
        }

        if (hostileAI != null)
        {
            hostileAI.enabled = false;
        }
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce((-transform.forward + Vector3.up) * flyForce, ForceMode.Impulse);
            rb.AddTorque(Vector3.right * 10f, ForceMode.Impulse);
        }

        Invoke(nameof(DestroyEnemy), 10f);
    }

    void StopCameraShake()
    {
        if (CameraShake != null)
        {
            CameraShake.StopFeedbacks();
        }
    }
    
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}