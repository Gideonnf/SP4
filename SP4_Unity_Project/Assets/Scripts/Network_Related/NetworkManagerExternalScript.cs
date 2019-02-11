using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerExternalScript : NetworkManager//MonoBehaviour
{
    [SerializeField]
    private List<NetworkConnection> connectedClients = new List<NetworkConnection>();


    // When a new client tries to connect to this server
    public override void OnServerConnect(NetworkConnection newConnection)
    {
        //if (Conn.hostId >= 0)
        //{
            Debug.Log("New Player has joined of IP: " + newConnection.address);
        //}

        connectedClients.Add(newConnection);
    }

    // When an existing client wants to disconnect from server
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // Your code here
        Debug.Log("Player has LEFT");

        foreach (NetworkConnection connection in connectedClients)
        {
            if(conn == connection)
            {
                //ClientLeft(connection.address);
            }
        }
    }


    ////Detect when a client connects to the Server
    //public override void OnClientConnect(NetworkConnection connection)
    //{
    //    Debug.Log("A Client Connected");
    //}


    ////Detect when a client connects to the Server
    ////This is called on the client when it disconnects from the server
    //public override void OnClientDisconnect(NetworkConnection connection)
    //{
    //    Debug.Log("A Client Disconnected");
    //}

    // [ClientRpc] makes sures that the Function will ONLY BE 
    // Executed on the Clients.
    //[ClientRpc]
    //void ClientLeft(string clientIP)
    //{
    //    Debug.Log("Delete Leaving Client's Stuff");
    //}
}
