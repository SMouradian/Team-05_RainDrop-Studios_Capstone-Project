using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject HitParticle;
    public int damageAmount = 25;

    private Collider hitboxCollider;
    private HashSet<EnemyHealth> enemiesHitThisSwing = new HashSet<EnemyHealth>();

    
    private void Start()
    {
        Debug.Log("CollisionDetection script started on " + gameObject.name);
    }
    private void Awake()
    {
        hitboxCollider = GetComponent<Collider>();
        if (hitboxCollider == null)
        {
            hitboxCollider = GetComponentInChildren<Collider>();
        }
        if (hitboxCollider == null)
        {
            Debug.LogError("Collider component not found on " + gameObject.name);
            return;
        }

        Debug.Log("Collider found on " + gameObject.name + ": " + hitboxCollider.name);
        hitboxCollider.isTrigger = true;
        hitboxCollider.enabled = false;
    }

    public void EnableHitbox()
    {
        enemiesHitThisSwing.Clear();
        hitboxCollider.enabled = true;
        Debug.Log("Hitbox enabled");
    }

    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
        Debug.Log("Hitbox disabled");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hitboxCollider.enabled)
            return;
        if (!other.CompareTag("Enemy"))
            return;  

        EnemyHealth eh = other.GetComponentInParent<EnemyHealth>();
        if (eh == null)
            return;

        if (!enemiesHitThisSwing.Add(eh))
            return;

        Debug.Log("Hit enemy: " + other.name);
        eh.TakeDamage(damageAmount);

        if (HitParticle != null)
        {
            Instantiate(HitParticle, other.ClosestPoint(transform.position), Quaternion.identity);
        }      
    }   
}
