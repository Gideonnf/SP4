using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionHandler : NetworkBehaviour //MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefabToInstantiate = null;
    public GameObject attachedPlayerObject = null;
    public string playerName = "NormalName";


    // Start is called before the first frame update
    void Start()
    {
        // Is this actually my own local PlayerObject??
        if (!isLocalPlayer)
        {
            // This Object belongs to another player
            return;
        }

        // if there is a Prefab to create...
        if (playerPrefabToInstantiate)
        {
            // Since the PlayerHandler is an empty gameObject and not currently controlled
            // By the player, Give the player something phyiscal to move around!!!
            Debug.Log("Creating PlayerPrefab...");

            // Instantiate() only creates an object on the LOCAL COMPUTER
            // Even if it has a NetworkIdentity, it will still NOT EXIST on
            // the network (therefore not on any other connected clients UNLESS
            // NetworkServer.Spawn() is called on this Object.
            // NOTE: NetworkServer.Spawn() can ONLY be called by the Server or Host!!

            // Command the Server to spawn our player unit
            CmdSpawnMyPlayer();

            //Instantiate(playerPrefabToInstantiate);
        }
        else
        {
            Debug.Log("FAILED TO CREATE PLAYER PREFAB!!!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Update runs on EVERYONE's computer, whether they own this
        // gameObject or not..


        // Is this actually my own local PlayerObject??
        if (!isLocalPlayer)
        {
            // This Object belongs to another player
            return;
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            string n = "Quill" + Random.Range(1, 100);
            Debug.Log("Sending the server a request to change our name to: " + n);
            CmdChangePlayerName(n);
        }
    }



    // [Command] makes sures that the Function will ONLY BE 
    // Executed on the Server
    [Command]   // We are guaranteed to be on the server now, basically client to server
    void CmdSpawnMyPlayer()
    {
        GameObject go = Instantiate(playerPrefabToInstantiate);
        attachedPlayerObject = go;
        attachedPlayerObject.transform.parent = this.transform;

        // Now that the GameObject exists on the server,
        // propagate it to all connected clients.

        // Assign the client's Autority to this newly created GO
        // So that the client can modify that GO locally
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]   // We are guaranteed to be on the server now, basically client to server
    void CmdChangePlayerName(string newName)
    {
        Debug.Log("CmdChangePlayerName: " + newName);
        // Modify server's copy of data
        // playerName = newName;

        // Tell rest of clients what this player's name now is.
        RpcChangePlayerName(newName);
    }



    // [ClientRpc] makes sures that the Function will ONLY BE 
    // Executed on the Clients.
    [ClientRpc]
    void RpcChangePlayerName(string newNameFromServer)
    {
        Debug.Log("RpcChangePlayerName: We were asked to change the player name from a particular GameObject to: " + newNameFromServer);
        this.playerName = newNameFromServer;
    }


}
