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
    public Light flashLight;
    public float flashDuration = 2f;

    private Rigidbody playerRigidbody;
    private Vector3 movePlayerAway = new Vector3 (2f, 0, 0);
    private bool playerIsOverlapping = false;

    private void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        flashLight.enabled = false;
    }

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
                flashLight.transform.position = player.transform.position;
                StartCoroutine(FlashEffect());
                playerIsOverlapping = false;
            }
        }
    }
    private IEnumerator FlashEffect()
    {
        flashLight.enabled = true;  // Flash light on
        yield return new WaitForSeconds(flashDuration);
        flashLight.enabled = false; // Flash light off
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
