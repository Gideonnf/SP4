using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerExternalScript : NetworkManager//MonoBehaviour
{
    //[SerializeField]
    //public List<GameObject> clientPlayerObjects = new List<GameObject>();
    //public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    //{
    //    GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    //    //player.GetComponent<Player>().color = Color.red;
    //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    //    clientPlayerObjects.Add(player);
    //}


    //[SerializeField]
    //private List<NetworkConnection> connectedClients = new List<NetworkConnection>();
    //// When a new client tries to connect to this server
    //// THIS IS ONLY EXECUTED IN THE SERVER
    //public override void OnServerConnect(NetworkConnection newConnection)
    //{
    //    //if (Conn.hostId >= 0)
    //    //{
    //        Debug.Log("New Player has joined of IP: " + newConnection.address);
    //    //}

    //    connectedClients.Add(newConnection);
    //}

    //// When an existing client wants to disconnect from server
    //// THIS IS ONLY EXECUTED IN THE SERVER
    //public override void OnServerDisconnect(NetworkConnection conn)
    //{
    //    // Your code here
    //    Debug.Log("Player has LEFT");

    //    foreach (NetworkConnection connection in connectedClients)
    //    {
    //        if(conn == connection)
    //        {
    //            //ClientLeft(connection.address);
    //            Debug.Log("Delete Leaving Client's Stuff");
    //            NetworkServer.DestroyPlayersForConnection(conn);
    //        }
    //    }
    //}


    public GameObject StartHostButton;
    public GameObject JoinGameButton;

    public GameObject IpAddress;

    public void StartUpHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIpAddress();
        SetPort();

        NetworkManager.singleton.StartClient();
    }


    void SetIpAddress()
    {
        string ipAddress = IpAddress.transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }
    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }


    private void OnLevelWasLoaded(int level)
    {
        // 0 is main menu here in build settingss
        if(level == 0)
        {
            //SetUpMenuSceneButtons();
            StartCoroutine(SetUpMenuSceneButtons());
        }
       
    }


    IEnumerator SetUpMenuSceneButtons()
    {
        // Prevent adding listener to old NetWorkManager
        yield return new WaitForSeconds(0.3f);

        // clear and reput event listeners if scene is reloaded
        StartHostButton.GetComponent<Button>().onClick.RemoveAllListeners();
        StartHostButton.GetComponent<Button>().onClick.AddListener(StartUpHost);

        JoinGameButton.GetComponent<Button>().onClick.RemoveAllListeners();
        JoinGameButton.GetComponent<Button>().onClick.AddListener(JoinGame);
    }
}
