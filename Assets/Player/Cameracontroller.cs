using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    public Getinput input;
    private Camera cam;
    public Transform spaceshipCam;
    public float catchupPosition;
    public float catchupRotation;
    private float pov;
    public float zoomedPov;
    public float zoomTime;

    void Start()
    {
        //gets camera component and changes pov to camera pov
        cam = GetComponent<Camera>();
        pov = cam.fieldOfView;
    }
    void FixedUpdate()
    {
        //moves and rotates camera smoothly behind the spaceship
        cam.transform.position = Vector3.Lerp(cam.transform.position, spaceshipCam.transform.position, catchupPosition);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, spaceshipCam.transform.rotation, catchupRotation);
        if (input.rightMouse && cam.fieldOfView != zoomedPov)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomedPov, zoomTime * Time.deltaTime);
        }
        else if (cam.fieldOfView != pov)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, pov, zoomTime * Time.deltaTime);
        }

        cursorLock();
    }

        void cursorLock()
        {
        //locks cursor
        Screen.lockCursor = true;
    }
    }