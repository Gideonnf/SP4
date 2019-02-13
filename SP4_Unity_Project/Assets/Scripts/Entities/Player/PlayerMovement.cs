using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour //MonoBehaviour
{
    public Rigidbody rb;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // if I am allowed to mess around with this GameObject, then do whatever..
        if( !hasAuthority )
        {
            return;
        }
        //// if still in lobby
        //if (GetComponent<PlayerID>().inLobby)
        //{
        //    return;
        //}


        // If spacebar was released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.transform.Translate(0, 1, 0);
        }
       
        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position - transform.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position - transform.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
        }
    }
}
