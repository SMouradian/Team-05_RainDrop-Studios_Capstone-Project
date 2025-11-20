using System.Collections;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    [Header("Dodge Settings")]
    [SerializeField] private KeyCode dodgeKey = KeyCode.LeftAlt;
    [SerializeField] private float dodgeDistance = 5f;
    [SerializeField] private float dodgeDuration = 0.25f;
    [SerializeField] private float dodgeCooldown = 1f;

    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Animator animator;
    [SerializeField] private MonoBehaviour movementScript;

    private bool isDodging = false;
    private bool canDodge = true;

    public bool IsDodging => isDodging;

    private void Update()
    {
        if (Input.GetKeyDown(dodgeKey) && canDodge)
        {
            float horizontal = Input.GetAxisRaw("Horizontal"); // A/D
            float vertical = Input.GetAxisRaw("Vertical");     // W/S

            Vector3 inputDirection = new Vector3(horizontal, 0, vertical);

            StartCoroutine(Dodge(inputDirection));
        }
    }

    private IEnumerator Dodge(Vector3 direction)
    {
        canDodge = false;
        isDodging = true;

        Transform basis = orientation != null ? orientation : transform;

        Vector3 dodgeDirection;
        if (direction.sqrMagnitude > 0.1f)
        {
            dodgeDirection = (basis.right * direction.x + basis.forward * direction.z).normalized;
            
        }
        else
        {
            dodgeDirection = -basis.forward;
        }

        if(movementScript != null)
        {
            movementScript.enabled = false;
        }

        if (animator != null)
        {
            animator.SetTrigger("Dodge");
        }

        float elapsed = 0f;
        float speed = dodgeDistance / dodgeDuration;
        while (elapsed < dodgeDuration)
        {
            Vector3 displacement = dodgeDirection * speed * Time.deltaTime;

            CharacterController controller = GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.Move(displacement);
            }
            else
            {
                transform.position += displacement;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if(movementScript != null)
        {
            movementScript.enabled = true;
        }

        isDodging = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }
}
