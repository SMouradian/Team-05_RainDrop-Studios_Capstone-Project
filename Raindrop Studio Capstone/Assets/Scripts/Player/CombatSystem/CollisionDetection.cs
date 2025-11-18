using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wp;
    public GameObject HitParticle;
    public int damageAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && wp.IsAttacking)
        {
            Debug.Log("Hit Enemy: " + other.name);

            EnemyHealth eh = other.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(damageAmount);

                if (HitParticle != null)
                {
                    Instantiate(HitParticle, other.ClosestPoint(transform.position), Quaternion.identity);
                }
            }
        }
    }
   
}
