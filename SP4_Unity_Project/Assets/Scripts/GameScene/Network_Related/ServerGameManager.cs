using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerGameManager : NetworkBehaviour
{

    public GameObject LootPrefab;
    public GameObject AnimalPrefab;

    public Transform[] LootSpawn;

    public List<GameObject> Lootboxes = new List<GameObject>();

    static public ServerGameManager gameManager;

    float nextTick = 0.0f;

    [SyncVar]
    float tickLength = 0.2f;

    [SyncVar]
    bool gameOver = false;

    void Awake()
    {
        gameManager = this;
        Lootboxes = new List<GameObject>();

        CreateAllLootbox();

    }

    public void ExitGame()
    {
        
    }

    public void CreateAllLootbox()
    {
        foreach (Transform location in LootSpawn)
        {
            CreateLootbox(LootPrefab, location.position);
        }
    }

    void CreateLootbox(GameObject prefab, Vector3 position)
    {
        GameObject box = (GameObject)Instantiate(prefab);
        box.transform.position = position;
        box.GetComponent<LootBox>().SetUp(position);
        Lootboxes.Add(box);

        NetworkServer.Spawn(box);
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
                CreateAllLootbox();
                tickLength = 0.2f;
            }
        }
    }


}
