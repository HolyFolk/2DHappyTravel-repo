using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    
    public Camera camera;
    public Transform subject;

    Vector2 startPosition;

    float startZ;

    Vector2 travel => (Vector2)camera.transform.position - startPosition;

    float distanceFromSubject => transform.position.z - subject.position.z;

    float clippingPlane => (camera.transform.position.z + (distanceFromSubject > 0 ? camera.farClipPlane : camera.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromSubject)/ clippingPlane;


    public void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    public void Update()
    {
        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }

}
