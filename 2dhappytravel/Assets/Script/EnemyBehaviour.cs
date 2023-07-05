using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyBehaviour : MonoBehaviour
{
    public float hitpoint;
    public float maxHit = 5f;
    private GameObject[] players;
    private GameObject playerOnLoad;
    private Transform playLoc;

    public bool isFlipped = false;
    public float attackDamage = 2.5f;
    public Vector3 attackOffset;
    public float attackRange = 2f;
    public LayerMask attackMask;

    private void Start()
    {
        hitpoint = maxHit;
        // GameObject playerOnLoad = GameObject.FindGameObjectWithTag("Player");
        // playLoc = playerOnLoad.GetComponent<Transform>();
    }

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        // get the position of the target (AKA player)
        foreach (GameObject target in targets)
        {
            playLoc = target.GetComponent<Transform>();
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > playLoc.transform.position.x && isFlipped) 
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        } else if(transform.position.x < playLoc.transform.position.x && !isFlipped) 
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
        if(colInfo != null)
        {
            colInfo.GetComponent<PlayerStat>().TakeDamage(attackDamage);
        }
    }

    public void TakeHit(float damage)
    {
        hitpoint -= damage;
        if (hitpoint <= 0) 
        { 
        Destroy(gameObject);
        } 
    }
}
