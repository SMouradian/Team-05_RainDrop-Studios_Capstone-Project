using UnityEngine;
using UnityEngine.Windows.WebCam;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && wc.isAttacking)
        {
            Debug.Log(other.name);
        }
    }
}
