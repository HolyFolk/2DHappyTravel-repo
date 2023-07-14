using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTSMove : StateMachineBehaviour
{
    public float speed = 4f;
    public float checkDistance = 10f;
    public float attackRange = 5f;
    private EnemyAI enemyAI;

    Transform player;
    Rigidbody2D rb;
    WTSHealth wtsHealth;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAI = animator.GetComponent<EnemyAI>();
        rb = animator.GetComponent<Rigidbody2D>();
        wtsHealth = animator.GetComponent<WTSHealth>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //wtsHealth.LookAtPlayer();
        //Vector2 target = new Vector2(player.position.x, rb.position.y);
        //Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        player = enemyAI.getTargetPlayer().transform;
        float distance = Vector2.Distance(player.position, rb.position);
        Debug.Log(distance);
       /* if (distance < checkDistance && distance > attackRange)
        {
            rb.MovePosition(newPos);
            Debug.Log(distance);
        }
        else*/ if (distance <= attackRange)
        {
            animator.SetTrigger("WTSAT");
        }
        else
        {
            animator.Play("WTSIdle");
            animator.ResetTrigger("WTSAT");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
