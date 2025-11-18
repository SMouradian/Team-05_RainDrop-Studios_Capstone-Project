using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponController : MonoBehaviour
{
    public GameObject Katana;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool IsAttacking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                KatanaAttack();
            }
        }
    }


    public void KatanaAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = Katana.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
        IsAttacking = false;
    }

}