using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceRun : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float checkDistance = 12f;
    public float attackRange = 3f;
    public GameObject[] players;

    // Transform player;
    Rigidbody2D rb;
    EnemyBehaviour enemyBehaviour;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemyBehaviour = animator.GetComponent<EnemyBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                enemyBehaviour.LookAtPlayer();
                Vector2 target = new Vector2(players[i].transform.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                float distance = Vector2.Distance(players[i].transform.position, rb.position);
                Debug.Log(distance);
                if (distance < checkDistance && distance > attackRange)
                {
                    rb.MovePosition(newPos);
                    Debug.Log(distance);
                }
                else if (distance <= attackRange)
                {
                    animator.SetTrigger("PoliceAT");
                }
                else
                {
                    animator.Play("PoliceIdle");
                    animator.ResetTrigger("PoliceAT");
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
