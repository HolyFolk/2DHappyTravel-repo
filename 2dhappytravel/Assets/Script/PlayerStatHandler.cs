using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


public class PlayerStatHandler : MonoBehaviour
{
    [SerializeField] PlayerStat playerStat;
    [SerializeField] GameObject player;
    public float hp = 10f;
    public float maxhp = 10f;
    public int lifeCount = 5;
    public HealthBarBehaviour healthBar;
    
    private Collider2D playerCollider;
    private bool isVulenerable = true;
    PlayerMovementV2 movement;
    bool gameOver = false;
    void Awake()
    {
        playerCollider = player.GetComponent<Collider2D>();
        movement = player.GetComponent<PlayerMovementV2>();
        if (playerStat.IsNull)
        {
            playerStat.HP = hp;
            playerStat.MaxHp = maxhp;
            playerStat.LifeCount = lifeCount;
            playerStat.IsNull = false;
        }

        hp = playerStat.HP;
        maxhp = playerStat.MaxHp;
        lifeCount = playerStat.LifeCount;
        

        healthBar.SetMaxHealth(maxhp);
        healthBar.SetHealth(hp);
    }

    public void FixedUpdate()
    {
        Debug.Log(isVulenerable && hp > 0);
    }

    public void TakeDamage(float amount)
    {

        if (isVulenerable == false)
        {
            return;
        }
        else if (isVulenerable && hp >0)
        {
            hp -= amount;
            playerStat.HP = hp;
            healthBar.SetHealth(hp);
        }
        

        /*if(gameObject.transform.position.y < -10)
        {
            TakeDamage(5);
        }*/

        if (hp <= 0 && lifeCount > 0)
        {
            Dead();
            Invoke("Respawn", 2f);
            lifeCount--;
            playerStat.LifeCount=lifeCount;
        } else if(hp <= 0 && lifeCount == 0){
            playerStat.IsNull = true;
            gameOver = true;
            Dead();
            player.transform.position = new Vector2(-200, -999);
        }
    }

    public void Dead()
    {
        isVulenerable=false;
        movement.enabled = false;
        movement.animator.enabled = false;
        playerCollider.enabled= false;
        transform.Rotate(0, 0, 90);
        movement.RB.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    public void Respawn()
    {
        isVulenerable = true;
        transform.Rotate(0, 0, -90);
        movement.animator.enabled = true;
        movement.RB.constraints = RigidbodyConstraints2D.None;
        movement.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.enabled = true;
        playerCollider.enabled = true;
        transform.position = playerStat.RespawnPoint;
        hp = maxhp;
        playerStat.HP = hp;
        healthBar.SetHealth(maxhp);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            playerStat.RespawnPoint = collision.gameObject.transform.position;
        }
    }

    public bool isGameOver()
    {
        return gameOver;
    }
    
}
