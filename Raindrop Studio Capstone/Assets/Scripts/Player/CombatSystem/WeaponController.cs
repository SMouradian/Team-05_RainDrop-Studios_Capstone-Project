using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponController : MonoBehaviour
{
    public GameObject Katana;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public float AttackHitWindow = 0.3f;
    private Animator anim;
    private CollisionDetection hitbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        hitbox = Katana.GetComponent<CollisionDetection>();
        anim = Katana.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            KatanaAttack();
        }
    }


    public void KatanaAttack()
    {
        CanAttack = false;
        if(anim != null)
        {
            anim.SetTrigger("Attack");
        }

        if (hitbox != null)
        {
            StartCoroutine(HitWindow());
        }
        StartCoroutine(ResetAttackCooldown());
    }

    private IEnumerator HitWindow()
    {
        hitbox.EnableHitbox();
        yield return new WaitForSeconds(AttackHitWindow);
        hitbox.DisableHitbox();
    }
    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
}