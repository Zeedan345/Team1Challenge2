using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalTelportor : MonoBehaviour
{
    public GameObject player;
    public bool isHome;
    public Transform reciever;
    public AudioSource teleportSound;

    private Rigidbody playerRigidbody;
    private Vector3 movePlayerAway = new Vector3 (2f, 0, 0);
    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.transform.position - transform.position;
            float dotProduct = -Vector3.Dot(transform.up, portalToPlayer);
            //True if Player has crossed
            if (dotProduct < 0f)
            {
                
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.transform.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                teleportSound.Play();
                if (isHome)
                {
                    player.transform.position = reciever.position + positionOffset - movePlayerAway;
                }
                else
                {
                    player.transform.position = reciever.position + positionOffset + movePlayerAway;
                }
                
                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            playerIsOverlapping = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}
