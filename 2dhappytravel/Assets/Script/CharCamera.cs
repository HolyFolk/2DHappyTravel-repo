using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class CharCamera : MonoBehaviour
{

    // the player prefab
    public GameObject playerPrefab;

    // the camera distance (z position)
    public float distance = -10f;

    // the height the camera should be above the target (AKA player)
    public float height = 0f;

    // damping is the amount of time the camera should take to go to the target
    public float damping = 5.0f;

    // map maximum X and Y coordinates. (the final boundaries of your map/level)
    public float mapX = 100.0f;
    public float mapY = 100.0f;

    // just private var for the map boundaries
    private float minX = 0f;
    private float maxX = 0f;
    private float minY = 0f;
    private float maxY = 0f;

    public List<Transform> playerTransforms;

    void Start()
    {
        // the map MinX and MinY are the position that the camera STARTS
        minX = transform.position.x;
        minY = transform.position.y;
        // the desired max boundaries
        maxX = mapX;
        maxY = mapY;

        // instantiate a camera for each player object in the scene
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            CreatePlayerCamera(player);
        }
    }

    public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        // a new player has entered the room
        // instantiate a camera for the new player object in the scene
        GameObject player = PhotonView.Find((int)newPlayer.CustomProperties["playerId"]).gameObject;
        CreatePlayerCamera(player);
    }

    void CreatePlayerCamera(GameObject player)
    {
        GameObject cameraObj = new GameObject("PlayerCamera");
        cameraObj.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, distance);
        cameraObj.transform.parent = transform;

        Camera camera = cameraObj.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Color.black;
        camera.depth = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
        camera.orthographic = true;
        camera.orthographicSize = 8;

        CharCameraFollow followScript = cameraObj.AddComponent<CharCameraFollow>();
        followScript.target = player.transform;
        followScript.height = height;
        followScript.damping = damping;
        followScript.minX = minX;
        followScript.maxX = maxX;
        followScript.minY = minY;
        followScript.maxY = maxY;
    }


    /*
    void Update()
    {

        // iterate through the list of player character transforms
        foreach (Transform playerTransform in playerTransforms)
        {
            // get the position of the target (AKA player)
            Vector3 wantedPosition = playerTransform.TransformPoint(0, height, distance);



            // check if it's inside the boundaries on the X position
            wantedPosition.x = (wantedPosition.x < minX) ? minX : wantedPosition.x;
            wantedPosition.x = (wantedPosition.x > maxX) ? maxX : wantedPosition.x;

            // check if it's inside the boundaries on the Y position
            wantedPosition.y = (wantedPosition.y < minY) ? minY : wantedPosition.y;
            wantedPosition.y = (wantedPosition.y > maxY) ? maxY : wantedPosition.y;

            // set the camera to go to the wanted position in a certain amount of time
            transform.position = Vector3.Lerp(transform.position, wantedPosition, (Time.deltaTime * damping));
        }
    }
    */
}