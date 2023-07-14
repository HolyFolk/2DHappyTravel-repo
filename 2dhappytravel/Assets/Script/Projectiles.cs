using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float pSpeed = 4.5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * pSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<EnemyState>();
        var boss = collision.collider.GetComponent<WTSHealth>();
        if (enemy)
        {
            enemy.TakeDamage(1.2f);
        }else if(boss)
        { 
            boss.TakeDamage(1.2f);       
        }
        Destroy(gameObject);
    }
}
