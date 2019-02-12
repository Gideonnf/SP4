using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : NetworkBehaviour
{
    public List<GameObject> Lootboxes = new List<GameObject>();
    static public ServerManager serverManager;

    float nextTick = 0.0f;

    [SyncVar]
    float tickLength = 0.2f;

    [SyncVar]
    bool gameOver = false;


    // Start is called before the first frame update
    void Awake()
    {
        serverManager = this;
        Lootboxes = new List<GameObject>();

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    [ServerCallback]
    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (Time.time >= nextTick)
        {
            nextTick = Time.time + tickLength;

            bool foundBoxes = false;

            foreach (GameObject Lootbox in Lootboxes)
            {
                if (Lootbox == null)
                    continue;

                Lootbox.GetComponent<LootBox>().UpdateLootbox();
                // Lootbox.transform.Translate(0, 0.1f, 0);
                // Lootbox.GetComponent<NetworkTransform>().SetDirtyBit(1);
                foundBoxes = true;
            }

            if (foundBoxes == false)
            {
                if(GameManager.gameManager)
                {
                    GameManager.gameManager.CreateAllLootbox();
                   //CreateAllLootbox();
                }
                tickLength = 0.2f;
            }
        }

    }
}
