using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyBehaviour : MonoBehaviourPun, IPunObservable
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

    public float moveSpeed = 5f;
    public float detectionRange = 10f;

    public GameObject target;

    private void Start()
    {
        if (photonView.IsMine)
        {
            hitpoint = maxHit;
            // GameObject playerOnLoad = GameObject.FindGameObjectWithTag("Player");
            // playLoc = playerOnLoad.GetComponent<Transform>();
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            /*
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

            // get the position of the target (AKA player)
            foreach (GameObject target in targets)
            {
                playLoc = target.GetComponent<Transform>();
            }
            */

            // Find the nearest player

            float nearestDistance = Mathf.Infinity;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player != null && photonView.IsMine)
                {
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        target = player;
                    }
                }
            }

            // Move towards the nearest player
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > target.transform.position.x && isFlipped) 
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the enemy's state across the network
            stream.SendNext(hitpoint);
            stream.SendNext(isFlipped);
            stream.SendNext(target);
        }
        else
        {
            // Receive the enemy's state from the network
            hitpoint = (float)stream.ReceiveNext();
            isFlipped = (bool)stream.ReceiveNext();
            target = (GameObject)stream.ReceiveNext();
        }
    }
}
