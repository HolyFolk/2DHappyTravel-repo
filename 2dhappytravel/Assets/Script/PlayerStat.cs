using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public float hp;
    public float maxhp = 10;
    public int lifeCount = 1;
    public HealthBarBehaviour healthBar;
    public PlayerMovement movement;
    public Text goText;
    private Collider2D playerCollider;
    void Start()
    {
        hp = maxhp;
        healthBar.SetMaxHealth(maxhp);
        healthBar.SetHealth(hp);

        GameObject player = GameObject.Find("Player");
        playerCollider = player.GetComponent<Collider2D>();
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
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
        } else if(hp <= 0 && lifeCount == 0){
            Dead();
            GO();
        }
    }

    public void Dead()
    {
        movement.canMove = false;
        movement.animator.enabled = false;
        playerCollider.enabled= false;
        transform.Rotate(0, 0, 90);
        movement.rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    public void Respawn()
    {
        movement.animator.enabled = true;
        transform.Rotate(0, 0, -90);
        movement.rb.constraints = RigidbodyConstraints2D.None;
        movement.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        movement.canMove = true;
        playerCollider.enabled = true;
        transform.position = movement.respawnPoint;
        hp = maxhp;
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
            movement.respawnPoint = collision.gameObject.transform.position;
        }
    }
}
