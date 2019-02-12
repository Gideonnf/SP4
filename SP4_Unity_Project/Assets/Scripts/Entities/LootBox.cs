using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LootBox : NetworkBehaviour
{
    public bool alive = true;

    public GameObject powerupPrefab;

    public float offset = 2.0f;
    private float maxY, minY;
    private static bool floatDown = false;
    
   // [ClientCallback]
    public override void OnStartClient()
    {
        if (!ServerManager.serverManager.Lootboxes.Contains(this.gameObject))
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

    public void UpdateLootbox()
    {
        // Lootbox.transform.Translate(0, 0.1f, 0);
        // Lootbox.GetComponent<NetworkTransform>().SetDirtyBit(1);
        if (floatDown == false)
        {
           // this.transform = 
            this.transform.Translate(0, 2.0f * Time.deltaTime, 0);
        }
        else
        {
            this.transform.Translate(0, -2.0f * Time.deltaTime, 0);
        }

        if (transform.position.y >= maxY)
        {   // Reached the highest point
            floatDown = true;
        }
        else if (transform.position.y <= minY)
        { // Reached the lowest point
            floatDown = false;
        }

        GetComponent<NetworkTransform>().SetDirtyBit(1);
    }

    public void SetUp(Vector3 Position)
    {
        maxY = Position.y + offset;
        minY = Position.y - offset;
    }

    void Update()
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
