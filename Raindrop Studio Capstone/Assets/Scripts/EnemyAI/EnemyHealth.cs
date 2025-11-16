using UnityEngine;
using UnityEngine.AI;
using MoreMountains.Feedbacks;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth = 100;
    public MMFeedbacks CameraShake;
    public ParticleSystem PS;
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

