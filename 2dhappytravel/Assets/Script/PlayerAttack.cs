using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region variable for storing attack Keys
    [SerializeField]
    private KeyCode melee = KeyCode.J;
    [SerializeField]
    private KeyCode ranged = KeyCode.K;
    #endregion
    void Update()
    {
    #region Attack Key
        if (attackCD <= 0)
        {
            if(Input.GetKey(melee))
            {
                animator.SetTrigger("Melee");
                enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            }
            else if(Input.GetKey(ranged))
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
        if (attackCD <= 0)
        {
            
            animator.SetTrigger("Melee");
            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            attackCD = startTimeBtwAttack;
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }

    public void TriggerRanged()
    {
        if (attackCD <= 0)
        {
            
            animator.SetTrigger("Ranged");
            attackCD = startTimeBtwAttack;
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
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
                enemiesToDamage[i].GetComponent<EnemyState>().TakeDamage(damage);
            }
        }
    }

    void Shoot()
    {
        Instantiate(projectile, aimPoint.position, aimPoint.rotation);
    }
}
