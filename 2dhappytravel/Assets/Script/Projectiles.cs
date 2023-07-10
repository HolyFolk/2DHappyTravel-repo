using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float pSpeed = 4.5f;
    public Rigidbody2D rb;
    private 

    void Start()
    {
        rb.velocity = transform.right * pSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<EnemyState>();
        if (enemy)
        {
            enemy.TakeDamage(1.2f);
        }
        Destroy(gameObject);
    }
}
