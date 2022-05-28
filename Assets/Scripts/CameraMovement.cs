using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 originalPos;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    private float camMinX, camMaxX, camMinY, camMaxY;

    private Vector3 dragOrigin;

    public Vector3 getOriginalPos(){
        return this.originalPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;

        camMaxX = 7;
        camMinX = -7;

        camMaxY = 4;
        camMinY = -4;
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
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }

    }

    private void Zoom()
    {
        float newSize = cam.orthographicSize - (Input.GetAxis("Mouse ScrollWheel") * zoomStep);

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPos){
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = camMinX + camWidth;
        float maxX = camMaxX - camWidth;

        float minY = camMinY + camHeight;
        float maxY = camMaxY - camHeight;

        float newX = Mathf.Clamp(targetPos.x, minX, maxX);
        float newY = Mathf.Clamp(targetPos.y, minY, maxY);

        return new Vector3(newX, newY, targetPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        Zoom();
    }

}
