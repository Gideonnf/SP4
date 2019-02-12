using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LootBox : NetworkBehaviour
{
    public bool alive = true;

    public GameObject powerupPrefab;

    public override void OnStartClient()
    {
        if (!ServerGameManager.gameManager.Lootboxes.Contains(this.gameObject))
        {
            ServerGameManager.gameManager.Lootboxes.Add(this.gameObject);
        }
    }

    public void Destroyed()
    {
        if(NetworkServer.active && alive == false)
        {
            GameObject powerUp = GameObject.Instantiate(powerupPrefab);
            NetworkServer.Spawn(powerUp);
        }
    }

    void FixedUpdate()
    {
       // this.transform.Translate(0, 0.1f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (NetworkServer.active == false)
        {
            return;
        }


    }

}
