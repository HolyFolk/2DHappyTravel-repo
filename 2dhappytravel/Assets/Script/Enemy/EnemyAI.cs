using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public GameObject enemyGfx;
  

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private GameObject[] players;
    private GameObject targetPlayer;
    private Transform target;

    Seeker seeker;
    Rigidbody2D rb;

    bool isFacingRight = true;
    
    // Start is called before the first frame update

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        players= GameObject.FindGameObjectsWithTag("Player");
        targetPlayer = players[0];
        target = players[0].transform;
    }

    void UpdatePath()
    {
        for (int i = 0; i < players.Length; i++)
        {
            float targetDistance = Vector2.Distance(rb.position, players[i].transform.position);
            if (targetDistance < Vector2.Distance(rb.position, target.position))
            {
                target = players[i].transform;
                targetPlayer = players[i];
            }
        }
        if (seeker.IsDone())
           seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p){
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;    
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction *speed *Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(force.x >= 0.01f && !isFacingRight)
        {
           Flip();
            //isFacingLeft = false;
        }
        else if (force.x <= -0.01f && isFacingRight)
        {
            Flip();
            //isFacingLeft = true;
        }


    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0);
        isFacingRight = !isFacingRight;
    }

    public GameObject getTargetPlayer()
    {
        return targetPlayer;
    }
}
