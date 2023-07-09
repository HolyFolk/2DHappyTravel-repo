using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class WTSHealth : MonoBehaviour
{
    public float hitpoint;
    public float maxHit = 60;
    private Transform player;

    public bool isFlipped = false;
    public float attackDamage = 2.5f;
    public Vector3 attackOffset;
    public float attackRange = 2f;
    public LayerMask attackMask;
    public Slider slider;

    private void Start()
    {
        hitpoint = maxHit;
        SetMaxHealth(maxHit);
        SetHealth(maxHit);
        GameObject playLoc = GameObject.Find("Player");
        player = playLoc.GetComponent<Transform>();

    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerStat>().TakeDamage(attackDamage);
        }
    }

    public void TakeHit(float damage)
    {
        hitpoint -= damage;
        SetHealth(hitpoint);
        if (hitpoint <= 0)
        {
            //Dead Effect
            Invoke("Die", 3f);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
