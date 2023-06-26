using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float pSpeed = 4.5f;
    public Rigidbody2D rb;

    // Update is called once per frame
    private void Start()
    {   
        rb.velocity = transform.right * pSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<EnemyBehaviour>();
        if(enemy)
        {
            enemy.TakeHit(1);
        }
        Destroy(gameObject);
    }
}
