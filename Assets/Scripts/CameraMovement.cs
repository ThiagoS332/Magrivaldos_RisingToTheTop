using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    private Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        Zoom();
    }

    private void PanCamera()
    {
        // save pos of the mouse in world space whan drag starts (first time clicked)
        if(Input.GetMouseButtonDown(1)) {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        // calculate distance between dragOrigin and new pos if the button it is still pressed

        if(Input.GetMouseButton(1)) {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            // move the camera by that distance
            cam.transform.position += difference;
        }

    }

    private void Zoom()
    {
        float newSize = cam.orthographicSize - (Input.GetAxis("Mouse ScrollWheel") * zoomStep);

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

}
