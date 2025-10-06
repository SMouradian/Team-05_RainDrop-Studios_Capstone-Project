using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class moveableObject : MonoBehaviour
{
    public float pushForce = 1;


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody obj = hit.collider.attachedRigidbody;

        if (obj != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            obj.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
        }
    }
    
}
