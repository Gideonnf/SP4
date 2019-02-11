using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPlayerScrpt : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefabToInstantiate = null;


    // Start is called before the first frame update
    void Start()
    {
        // if there is a Prefab to create...
        if (playerPrefabToInstantiate)
        {
            // Since the PlayerHandler is an empty gameObject and not currently controlled
            // By the player, Give the player something phyiscal to move around!!!
            Debug.Log("Creating PlayerPrefab...");
            Instantiate(playerPrefabToInstantiate);
        }
        else
        {
            Debug.Log("FAILED TO CREATE PLAYER PREFAB!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
