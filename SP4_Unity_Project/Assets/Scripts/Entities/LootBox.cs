using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LootBox : NetworkBehaviour
{
    public bool alive = true;

    public GameObject powerupPrefab;
    private Rigidbody rb;

    public float offset = 2.0f;
    public float rotateSpeed = 5f;
    private Vector3 m_EulerAngleVelocity;
    private float maxY, minY;
    private static bool floatDown = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(0, 25, 0);
    }

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

    }

    public void SetUp(Vector3 Position)
    {
        maxY = Position.y + offset;
        minY = Position.y - offset;
    }

    void Update()
    {
        // this.transform.Translate(0, 0.1f, 0);
        // Lootbox.transform.Translate(0, 0.1f, 0);
        // Lootbox.GetComponent<NetworkTransform>().SetDirtyBit(1);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        //transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); 

        if (floatDown == false)
        {
            // this.transform = 
            rb.MovePosition(transform.position + transform.up * Time.deltaTime);
            // this.transform.Translate(0, 2.0f * Time.deltaTime, 0);
        }
        else
        {
            rb.MovePosition(transform.position - transform.up  * Time.deltaTime);
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

    void OnTriggerEnter(Collider other)
    {
        if (NetworkServer.active == false)
        {
            return;
        }


    }

}
