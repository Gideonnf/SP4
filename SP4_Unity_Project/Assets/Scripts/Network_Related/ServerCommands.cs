using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerCommands : NetworkBehaviour //MonoBehaviour
{
    // [Command] makes sures that the Function will ONLY BE 
    // Executed on the Server
    [Command]   // We are guaranteed to be on the server now, basically client to server
    public void CmdSpawnMyPlayer(GameObject playerPrefabToInstantiate)
    {
        GameObject go = Instantiate(playerPrefabToInstantiate);

        // Now that the GameObject exists on the server,
        // propagate it to all connected clients.

        // Assign the client's Autority to this newly created GO
        // So that the client can modify that GO locally
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]   // We are guaranteed to be on the server now, basically client to server
    public void CmdChangePlayerName(string newName)
    {
        Debug.Log("CmdChangePlayerName: " + newName);
        // Modify server's copy of data
        // playerName = newName;

        // Tell rest of clients what this player's name now is.
        //RpcChangePlayerName(newName);
    }


    //// [ClientRpc] makes sures that the Function will ONLY BE 
    //// Executed on the Clients.
    //[ClientRpc]
    //void RpcChangePlayerName(string newNameFromServer)
    //{
    //    Debug.Log("RpcChangePlayerName: We were asked to change the player name from a particular GameObject to: " + newNameFromServer);
    //    this.playerName = newNameFromServer;
    //}
}
