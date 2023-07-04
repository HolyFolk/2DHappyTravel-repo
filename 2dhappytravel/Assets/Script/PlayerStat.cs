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
    private GameObject currentTeleporter;

    void Start()
    {
        hp = maxhp;
        healthBar.SetMaxHealth(maxhp);
        healthBar.SetHealth(hp);

        GameObject player = this.gameObject;
        playerCollider = player.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (currentTeleporter != null)
        {
            transform.position = currentTeleporter.GetComponent<S1Portal>().GetDestination().position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
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
