using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform aimPoint;
    public GameObject projectilePrefab;
    private Animator animator;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Ranged");
            Invoke("Shoot", 0.1f);
        }
        else if(Input.GetKeyDown(KeyCode.J))
        {
            
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, aimPoint.position, aimPoint.rotation);
    }
}
