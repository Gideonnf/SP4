using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (this.transform.localScale.x > 2)
        //    this.transform.localScale -= new Vector3(0.05F, 0, 0.05F);
    }

    public Vector3 GetScale()
    {
        return this.transform.localScale;
    }

    public Vector3 GetPos()
    {
        return this.transform.position;
    }
}
