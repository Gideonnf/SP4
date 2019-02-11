using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerStart : NetworkBehaviour//MonoBehaviour
{
    public List<PlayerController> connectedPlayerList;


    // Start is called before the first frame update
    void Start()
    {
        NetworkManager networkManager = NetworkManager.singleton;

        connectedPlayerList = networkManager.client.connection.playerControllers;

        for (int i = 0; i < connectedPlayerList.Count; i++)
        {
            if (connectedPlayerList[i].IsValid)
                Debug.Log(connectedPlayerList[i].gameObject.name);
        }
    }
}
