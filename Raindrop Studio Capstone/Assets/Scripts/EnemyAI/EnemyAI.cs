using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class HostileAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;


    [Header("Layers")]
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask playerLayerMask;


    [Header("Patrol Settings")]
    [SerializeField] private float patrolRadius = 10f;
    private Vector3 currentPatrolPoint;
    private bool hasPatrolPoint;


    [Header("Melee Attack Settings")]
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 90f;
    [SerializeField] private float attackWindupTime = 0.50f;
    [SerializeField] private Animator animator;

    [Header("Attack Telegraph")]
    [SerializeField] private Renderer telegraphRenderer;
    [SerializeField] private Material telegraphMaterial;
    private Material originalMaterial;

    [Header("Combat Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool isOnAttackCooldown;
    [SerializeField] private float forwardShotForce = 10f;
    [SerializeField] private float verticalShotForce = 5f;


    [Header("Detection Ranges")]
    [SerializeField] private float visionRange = 20f;
    [SerializeField] private float engagementRange = 10f;


    private bool isPlayerVisible;
    private bool isPlayerInRange;


    private void Awake()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }


        if (navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        if (telegraphRenderer == null)
        {
            telegraphRenderer = GetComponentInChildren<Renderer>();

            if (telegraphRenderer != null)
            {
                Debug.Log("Auto-assigned telgraphRenderer.");
            }
            else
            {
                Debug.LogWarning("Telegraph Renderer not assigned and could not be found in children.");
            }
        }

        if (telegraphRenderer != null)
        {
            originalMaterial = telegraphRenderer.material;
        }        
    }

    private void EnableTelegraph()
    {
        if (telegraphRenderer == null || telegraphMaterial == null)
        {
            Debug.LogWarning("Telegraph Renderer not assigned.");
            return;
        }

        //Debug.Log("Enabling telegraph effect.");
        telegraphRenderer.material = telegraphMaterial;
    }

    private void DisableTelegraph()
    {
        if (telegraphRenderer == null || originalMaterial == null) return;
        //Debug.Log("Disabling telegraph effect.");
        telegraphRenderer.material = originalMaterial;
    }

    private void Update()
    {
        DetectPlayer();
        UpdateBehaviourState();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);


        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }


    private void DetectPlayer()
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }


    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;


        Rigidbody projectileRb = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);
        projectileRb.AddForce(transform.up * verticalShotForce, ForceMode.Impulse);


        Destroy(projectileRb.gameObject, 3f);
    }


    private void FindPatrolPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);


        Vector3 potentialPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);


        if (Physics.Raycast(potentialPoint, -transform.up, 2f, terrainLayer))
        {
            currentPatrolPoint = potentialPoint;
            hasPatrolPoint = true;
        }
    }


    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }




    private void PerformPatrol()
    {
        if (!hasPatrolPoint)
            FindPatrolPoint();


        if (hasPatrolPoint)
            navAgent.SetDestination(currentPatrolPoint);


        if (Vector3.Distance(transform.position, currentPatrolPoint) < 1f)
            hasPatrolPoint = false;
    }


    private void PerformChase()
    {
        if (playerTransform != null)
        {
            navAgent.SetDestination(playerTransform.position);
        }
    }


    private void PerformAttack()
    {
        navAgent.SetDestination(transform.position);


        if (playerTransform != null)
        {
            Vector3 lookPos = playerTransform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
        }


        if (!isOnAttackCooldown)
        {
            StartCoroutine(MeleeAttackRoutine());
        }
    }

    private IEnumerator MeleeAttackRoutine()
    {
        isOnAttackCooldown = true;

        EnableTelegraph();

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        yield return new WaitForSeconds(attackWindupTime);

        if (playerTransform != null)
        {
            Vector3 toPlayer = playerTransform.position - transform.position;
            float distanceToPlayer = toPlayer.magnitude;
            
            if (distanceToPlayer <= attackRange)
            {
                float angleToPlayer = Vector3.Angle(transform.forward, toPlayer);
                if (angleToPlayer <= attackAngle / 2f)
                {
                    PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(attackDamage);
                        Debug.Log("Enemy attacked player for " + attackDamage + " damage.");
                    }
                }
            }
        }

        DisableTelegraph();

        yield return new WaitForSeconds(Mathf.Max(0f, attackCooldown - attackWindupTime));
        isOnAttackCooldown = false;
    }

    private void UpdateBehaviourState()
    {
        if (!isPlayerVisible && !isPlayerInRange)
        {
            PerformPatrol();
        }
        else if (isPlayerVisible && !isPlayerInRange)
        {
            PerformChase();
        }
        else if (isPlayerVisible && isPlayerInRange)
        {
            PerformAttack();
        }
    }
}
