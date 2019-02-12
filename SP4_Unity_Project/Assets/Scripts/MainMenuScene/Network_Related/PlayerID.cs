using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour//MonoBehaviour
{
    [SyncVar] public string playerUniqueName;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        GetNetIdentity();
        SetIdentity();
    }


    void Awake()
    {
        myTransform = transform;
        // DON'T DESTROY THIS PLAYER OBJECT WHEN CHANGING 
        // FROM Lobby to Game 
        DontDestroyOnLoad(myTransform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // If my name is empty or a clone of someone else's Player..
        if (myTransform.name == "" || myTransform.name == "ActualPlayerObject(Clone)")
        {
            SetIdentity();
        }
    }


    [Client]
    void GetNetIdentity()
    {
        // Create and send to server
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }
    string MakeUniqueIdentity()
    {
        string tempName = "Player " + playerNetID.ToString();
        return tempName;
    }

    [Client]
    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueName;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }


    [Command]
    void CmdTellServerMyIdentity(string myNewName)
    {
        // so synced with server
        playerUniqueName = myNewName;
    }
}
