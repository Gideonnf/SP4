using UnityEngine;

using UnityEngine.Networking;
using System.Collections;

public class CameraController : NetworkBehaviour
{
	// Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
	private Vector3 offset;

    GameObject[] players;
    GameObject player;

    // At the start of the game..
    void Start ()
	{
       
        // Create an offset by subtracting the Camera's position from the player's position
     
    }

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
    void LateUpdate ()
	{
        // Set the position of the Camera (the game object this script is attached to)
        // to the player's position, plus the offset amount

        if (player == null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; ++i)
            {
                // Is this actually my own local PlayerObject??
                if (players[i].GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    // This Object belongs to the local player
                    player = players[i];
                    offset = transform.position - player.transform.position;
                    transform.position = player.transform.position + offset;
                    return;
                }
            }
        }
        else
        {
            transform.position = player.transform.position + offset;
        }
    }
}