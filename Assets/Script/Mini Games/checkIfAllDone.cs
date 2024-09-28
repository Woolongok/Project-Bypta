using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfAllDone : MonoBehaviour
{
    public GameObject button;
    public GameObject blurCamera;
    public GameObject zoomCamera;
    Zoom zoom;
    blur blur;

    void Start()
    {
        blurCamera = GetComponent<GameObject>();
        zoomCamera = GetComponent<GameObject>();
        zoom = zoomCamera.GetComponent<Zoom>();
        blur = blurCamera.GetComponent<blur>();
    }

    void Update()
    {
        if (zoom.checkCameraZoom && blur.checkCameraBlur)
        {
            button.SetActive(true);
        }
    }
}
