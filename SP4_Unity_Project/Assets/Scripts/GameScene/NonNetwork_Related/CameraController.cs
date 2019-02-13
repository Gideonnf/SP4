using UnityEngine;

using UnityEngine.Networking;
using System.Collections;

public class CameraController : NetworkBehaviour
{
	// Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
	public Vector3 offset;

    GameObject[] players;
    GameObject localPlayer;

    // At the start of the game..
    void Start ()
	{
    }

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
    void LateUpdate ()
	{
        // Set the position of the Camera (the game object this script is attached to)
        // to the player's position, plus the offset amount

        if (localPlayer == null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; ++i)
            {
                // Is this actually my own local PlayerObject??
                if (players[i].GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    // This Object belongs to the local player
                    localPlayer = players[i];
                    //set camera starting location
                    transform.position = localPlayer.transform.position + offset;
                    return;
                }
            }
        }
        else
        {
            //set camera to follow player
            transform.position = localPlayer.transform.position + offset;
        }
    }
}