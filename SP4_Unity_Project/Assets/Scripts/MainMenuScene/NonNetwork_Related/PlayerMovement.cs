using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour //MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if I am allowed to mess around with this GameObject, then do whatever..
        if( !hasAuthority )
        {
            return;
        }

        // If spacebar was released
        if(Input.GetKeyUp(KeyCode.Space))
        {
            this.transform.Translate(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0, 0, 0.2f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0, 0, -0.2f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.2f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.2f, 0, 0);
        }
    }
}
