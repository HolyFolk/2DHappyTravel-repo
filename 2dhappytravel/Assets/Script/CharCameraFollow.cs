using UnityEngine;

public class CharCameraFollow : MonoBehaviour
{
    // the target object that the camera should follow
    public Transform target;

    // the distance (z position) of the camera from the target
    public float distance = -10f;

    // the height the camera should be above the target
    public float height = 0f;

    // damping is the amount of time the camera should take to go to the target
    public float damping = 5.0f;

    // map boundaries (the final boundaries of your map/level)
    public float minX = 0f;
    public float maxX = 0f;
    public float minY = 0f;
    public float maxY = 0f;

    void Update()
    {
        
        // get the position of the target object
        Vector3 wantedPosition = target.position;
        wantedPosition.z = distance;
        wantedPosition.y += height;

        // check if it's inside the boundaries on the X position
        wantedPosition.x = Mathf.Clamp(wantedPosition.x, minX, maxX);

        // check if it's inside the boundaries on the Y position
        wantedPosition.y = Mathf.Clamp(wantedPosition.y, minY, maxY);

        // set the camera to go to the wanted position in a certain amount of time
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
        
        /*
        Vector3 wantedPosition = target.TransformPoint(0, height, distance);

        // check if it's inside the boundaries on the X position
        wantedPosition.x = (wantedPosition.x < minX) ? minX : wantedPosition.x;
        wantedPosition.x = (wantedPosition.x > maxX) ? maxX : wantedPosition.x;

        // check if it's inside the boundaries on the Y position
        wantedPosition.y = (wantedPosition.y < minY) ? minY : wantedPosition.y;
        wantedPosition.y = (wantedPosition.y > maxY) ? maxY : wantedPosition.y;

        // set the camera to go to the wanted position in a certain amount of time
        transform.position = Vector3.Lerp(transform.position, wantedPosition, (Time.deltaTime * damping));
        */
    }

}