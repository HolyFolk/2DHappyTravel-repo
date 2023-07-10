using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject player;
    public float x;
    public float y;
    public float z;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Going S2");
            Teleport(x, y, z);
        }
    }

    void Teleport(float x, float y, float z)
    {
        player.transform.position = new Vector3(x, y, z);
    }
}
