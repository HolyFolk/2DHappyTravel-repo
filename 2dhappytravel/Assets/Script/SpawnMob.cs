using UnityEngine;
using System.Collections;

public class SpawnMob : MonoBehaviour
{

    public GameObject mob;
    private bool isNear;
    public Transform distanceCheck;
    public float checkRadius;
    public LayerMask player;

    void Update()
    {
        isNear = Physics2D.OverlapCircle(distanceCheck.position, checkRadius, player);
        if (isNear)
        {
            for(int i = 0; i < 5; i++) 
            {
                Duplicate();
                Debug.Log("Spawning");
            }
            Destroy(gameObject);
        }
    }

    void Duplicate()
    {
        GameObject clone = Instantiate(mob, transform.position, transform.rotation) as GameObject;
        Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), clone.GetComponent<Collider2D>());
    }
}