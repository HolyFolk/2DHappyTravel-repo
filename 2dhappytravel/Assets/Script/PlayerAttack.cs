using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackCD;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;

    void Update()
    {
        if(attackCD <= 0)
        {
            if(Input.GetKey(KeyCode.J)){
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    
                }
            }

            attackCD = startTimeBtwAttack;
        }
        else
        {
            attackCD -= Time.deltaTime;
        }
    }
}
