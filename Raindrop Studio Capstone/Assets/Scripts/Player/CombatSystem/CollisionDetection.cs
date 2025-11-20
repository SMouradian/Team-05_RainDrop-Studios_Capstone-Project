using System.Diagnostics;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wp;
    public GameObject HitParticle;
    private EnemyHealth EH;


    private void OnTriggerEnter(Collider other)
{
    if(other.tag == "Enemy" && wp.IsAttacking)
    {
        UnityEngine.Debug.Log(other.name);
        EnemyHealth eh = other.GetComponent<EnemyHealth>();
        if(eh != null)
        {
            eh.TakeDamage(wp.Damage);
        }
    }
}

}
