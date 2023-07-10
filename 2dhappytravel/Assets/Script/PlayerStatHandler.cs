using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatHandler : MonoBehaviour
{
    [SerializeField] PlayerStat playerStat;
    private float hp;
    private float maxhp;
    private int lifeCount;
    public HealthBarBehaviour healthBar;
    public PlayerMovementV2 movement;
    public Text goText;
    private Collider2D playerCollider;
    void Awake()
    {
       

        GameObject player = GameObject.Find("Player");
        playerCollider = player.GetComponent<Collider2D>();

        hp = playerStat.HP;
        maxhp = playerStat.MaxHp;
        lifeCount = playerStat.LifeCount;

        healthBar.SetMaxHealth(maxhp);
        healthBar.SetHealth(hp);
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        playerStat.HP =hp;
        healthBar.SetHealth(hp);

        if(gameObject.transform.position.y < -10)
        {
            TakeDamage(5);
        }

        if (hp <= 0 && lifeCount > 0)
        {
            Dead();
            Invoke("Respawn", 2f);
            lifeCount--;
            playerStat.LifeCount=lifeCount;
        } else if(hp <= 0 && lifeCount == 0){
            Dead();
            GO();
        }
    }

    public void Dead()
    {
        movement.enabled = false;
        movement.animator.enabled = false;
        playerCollider.enabled= false;
        transform.Rotate(0, 0, 90);
        movement.RB.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    public void Respawn()
    {
        movement.animator.enabled = true;
        transform.Rotate(0, 0, -90);
        movement.RB.constraints = RigidbodyConstraints2D.None;
        movement.RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.enabled = true;
        playerCollider.enabled = true;
        transform.position = playerStat.RespawnPoint;
        hp = maxhp;
        playerStat.HP = hp;
        healthBar.SetHealth(maxhp);
    }

    public void GO()
    {
        goText.gameObject.SetActive(true);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            playerStat.RespawnPoint = collision.gameObject.transform.position;
        }
    }
}
