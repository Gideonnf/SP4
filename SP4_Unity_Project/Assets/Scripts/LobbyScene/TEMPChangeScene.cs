using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TEMPChangeScene : NetworkBehaviour//MonoBehaviour
{
    public void NextSceneGame()
    {
        // Check if any of the players are already in game
        // If there are, then we should not join them..
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject localPlayer = null;
        for (int i = 0; i < players.Length; ++i)
        {
            // if player not in lobby
            if (!players[i].GetComponent<PlayerID>().inLobby)
            {
                return;
            }
            // Is this actually my own local PlayerObject??
            if (players[i].GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                // This Object belongs to the local player
                localPlayer = players[i];
            }
        }
        // If unable to locate LocalPlayer
        if (localPlayer == null)
            return;

        //SceneManager.LoadScene("GameScene");
        CmdTellServerToIntoGame(localPlayer.transform.name);
    }


    [Command]
    private void CmdTellServerToIntoGame(string nameOfClientThatPressedButton)
    {
        // loop through all connected clients and set them to be in game
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
            Debug.Log("NO PLAYERS DETECTED IN SERVER!!");
        for (int i = 0; i < players.Length; ++i)
        {
            players[i].GetComponent<PlayerID>().inLobby = false;
        }

        Debug.Log("CHANGING SCENE TO GAME");

        // Inform rest of clients to change scene
        RpcServerToldUsToGoToGame();
    }

    [ClientRpc]
    private void RpcServerToldUsToGoToGame()
    {
        // If already at game scene
        if (SceneManager.GetActiveScene().name == "GameScene")
            return;

        SceneManager.LoadScene("GameScene");
    }
}
