using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : NetworkBehaviour
{
    public Material good;
    public Material bad;
    public GameObject zone;

    // Start is called before the first frame update
    void Start()
    {      
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isLocalPlayer)
        //    return;

        if (Vector3.Distance(zone.GetComponent<ZoneController>().GetPos(), this.transform.position) > zone.GetComponent<ZoneController>().GetScale().x / 2)
        {
            if (GetComponent<Renderer>().material != bad)
            {
                GetComponent<Renderer>().material = bad;
            }
        }
        else
        {
            if (GetComponent<Renderer>().material != good)
            {
                GetComponent<Renderer>().material = good;
            }
        }
    }
}
