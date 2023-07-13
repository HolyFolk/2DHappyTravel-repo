using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float health = 5f;

    private AudioSource audioSource;
    public AudioClip dead;
    public GameObject bloodeffect;
    private EnemyAI enemyAI;
    private GameObject player;
    public PlayerStatHandler playerStatHandler;
    public float damage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatHandler = player.GetComponent<PlayerStatHandler>();
        audioSource = GetComponent<AudioSource>();
        enemyAI = GetComponent<EnemyAI>();
    }
    void Update()
    {
        player = enemyAI.getTargetPlayer();
        playerStatHandler = player.GetComponent<PlayerStatHandler>();
        if (health <= 0)
        {
            audioSource.PlayOneShot(dead);
            Invoke("Dead", 0.1f);
            Debug.Log("Dead");
        }
    }

    public void TakeDamage(float damage)
    {
        audioSource.Play();
        Instantiate(bloodeffect, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("Damage TAKEN !");
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        playerStatHandler.TakeDamage(damage);
    }
}
