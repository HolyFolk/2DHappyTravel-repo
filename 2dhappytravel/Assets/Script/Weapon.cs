using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviourPunCallbacks
{
    public Transform aimPoint;
    public GameObject projectilePrefab;
    private Animator animator;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }
    void Update()
    {
        if (photonView.IsMine && Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Ranged");
            photonView.RPC("ShootRPC", RpcTarget.All);
            // Invoke("Shoot", 0.3f);
            Invoke("ShootDelay", 0.3f);
        }
    }

    // void Shoot()
    // {
    //    Instantiate(projectilePrefab, aimPoint.position, aimPoint.rotation);
    // }

    void ShootDelay()
    {
        ShootRPC();
    }

    [PunRPC]
    void ShootRPC()
    {
        if (photonView.IsMine)
        {
            Instantiate(projectilePrefab, aimPoint.position, aimPoint.rotation);       
        }
    }
}