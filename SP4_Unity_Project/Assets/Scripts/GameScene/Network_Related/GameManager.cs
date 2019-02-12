using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public GameObject LootPrefab;
    public GameObject AnimalPrefab;

    public Transform[] LootSpawn;

    static public GameManager gameManager;

    float nextTick = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
        CreateAllLootbox();
    }

    void Start()
    {
    }

    public void CreateAllLootbox()
    {
        foreach (Transform location in LootSpawn)
        {
            CmdCreateLootbox(LootPrefab, location.position);
        }
    }

    //[Command]
    void CmdCreateLootbox(GameObject prefab, Vector3 position)
    {
        GameObject box = (GameObject)Instantiate(prefab);
        box.transform.position = position;
        box.GetComponent<LootBox>().SetUp(position);
        //ServerManager<a.Lootboxes.Add(box);
        ServerManager.serverManager.Lootboxes.Add(box);

        NetworkServer.Spawn(box);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
