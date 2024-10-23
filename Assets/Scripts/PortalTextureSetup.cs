using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera destinationCamera;
    public Material destinationView;
    // Start is called before the first frame update
    void Start()
    {
        if (destinationCamera.targetTexture != null) 
        {
            destinationCamera.targetTexture.Release();
        }
        destinationCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        destinationView.mainTexture = destinationCamera.targetTexture;
    }
}
