using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float attackCD = 0.2f;
    public float startTimeBtwAttack;

    public Transform aimPoint;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float damage;
    public Animator animator;
    private Collider2D[] enemiesToDamage;
    public GameObject projectile;

    public InputAction melee;
    public InputAction ranged;

    void Update()
    {
    #region Attack Key
        if (attackCD <= 0)
        {
            if(Input.GetKey(KeyCode.J))
            {
                animator.SetTrigger("Melee");
                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            }
            else if(Input.GetKey(KeyCode.K))
            {
                animator.SetTrigger("Ranged");
            }

            attackCD = startTimeBtwAttack;
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }

    public void TriggerMelee()
    {
        animator.SetTrigger("Melee");
        enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
    }

    public void TriggerRanged()
    {
        animator.SetTrigger("Ranged");
    }

    #endregion
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void Attack()
    { 
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i] != null)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    void Shoot()
    {
        Instantiate(projectile, aimPoint.position, aimPoint.rotation);
    }
}
