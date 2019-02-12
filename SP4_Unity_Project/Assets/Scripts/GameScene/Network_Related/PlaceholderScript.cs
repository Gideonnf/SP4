using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceholderScript : MonoBehaviour
{
    public List<GameObject> PlayerList = new List<GameObject>();
    private GameObject[] allObjects;
    public GameObject textList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //allObjects = Object.FindObjectsOfType<GameObject>();

        //foreach (GameObject go in allObjects)
        //{
        //    if (go.activeInHierarchy)
        //    {
        //        if (go.tag == "Player")
        //        {
        //            PlayerList.Add(go);
        //        }
        //    }
        //}

        //string newText = "";
        //foreach (GameObject go in PlayerList)
        //{
        //    newText += " " + go.transform.name;
        //}

        //textList.GetComponent<Text>().text = newText;
    }
}
