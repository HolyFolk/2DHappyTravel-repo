using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1Portal : MonoBehaviour
{

    /*
    GameObject player;

    void Teleport(float x, float y, float z)
    {
        player.transform.position = new Vector3(x, y, z);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Teleport(-6.31f, 35.24f, 1f);
        }
    }
    */

    [SerializeField] private Transform destination;

    public Transform GetDestination()
    {
        return destination;
    }
}


