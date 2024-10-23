using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalTelportor : MonoBehaviour
{
    public GameObject player;
    public GameObject Test;
    public Transform reciever;
    private Rigidbody playerRigidbody;
    public Vector3 movePlayerAway = new Vector3 (0, 0, 2f);
    private bool playerIsOverlapping = false;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        Test = GameObject.FindGameObjectWithTag("Test");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            Debug.Log(dotProduct);
            //True if Player has crossed
            if (dotProduct < 0f)
            {
                
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.transform.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.transform.position = reciever.position + positionOffset + movePlayerAway;
                Test.transform.position = reciever.position + positionOffset + movePlayerAway;
                Debug.Log(reciever.position);
                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Debug.Log("Entered");
            playerIsOverlapping = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exited");
            playerIsOverlapping = false;
        }
    }
}
