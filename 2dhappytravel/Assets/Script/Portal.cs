using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject[] players;
    public GameObject teleportTarget;
    private Vector3 teleportPos;


    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
       teleportPos= teleportTarget.transform.position;
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Going S2");
            Teleport(teleportPos);
        }
    }


    void Teleport(Vector3 tpPos)
    {
        for(int i = 0; i < players.Length; i++){
            players[i].transform.position = tpPos;  
        }
    }
}
