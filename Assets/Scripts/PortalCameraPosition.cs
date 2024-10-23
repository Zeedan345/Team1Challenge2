using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraPosition : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject destinationPortal;
    public GameObject homePortal;


    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.transform.position - homePortal.transform.position;
        transform.position = destinationPortal.transform.position + playerOffsetFromPortal;

        float angularDifferenceBetweenRotations = Quaternion.Angle(destinationPortal.transform.rotation, homePortal.transform.rotation);

        Quaternion portalRoationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenRotations, Vector3.up);

        Vector3 newCameraDirection = portalRoationalDifference * playerCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
