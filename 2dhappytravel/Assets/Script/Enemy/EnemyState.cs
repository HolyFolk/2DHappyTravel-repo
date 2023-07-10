using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float health = 5f;

    private AudioSource audioSource;
    public AudioClip dead;
    public GameObject bloodeffect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
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
}
