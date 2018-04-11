using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    public float scrollSpeed;
    public float rotationSpeed;
    public float panSpeed;
    public float minZoom;
    public float maxZoom;

    private Vector3 mouseOrigin;
    private Vector3 currentPosition;
    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private bool isPanning;
    private float yRotation;
    private float xRotation;




    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(initialRotation);
        currentPosition = transform.position;
        float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }
        // cancel on button release
        if (!Input.GetMouseButton(0))
        {
            isPanning = false;
        }
        //move camera on X & Y
        else if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            Vector3 move = new Vector3(pos.x * -panSpeed, pos.y * -panSpeed, 0);
            Camera.main.transform.Translate(move, Space.Self);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            transform.localPosition = new Vector3(transform.position.x, transform.position.y - (transform.position.y - (initialPosition.y)), transform.position.z);
        }
        //rotate camera with mousewheel click
        if (Input.GetMouseButton(2))
        {
            yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            yRotation = Mathf.Clamp(yRotation, -80, 80);
            xRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            xRotation = xRotation % 360;
            transform.localEulerAngles = new Vector3(yRotation + initialRotation.x, xRotation + initialRotation.y, 0);
        }
        //zoom with scroll
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && transform.position.y >= minZoom && transform.position.y <= maxZoom)
        {
            initialPosition = transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed * scrollAxis, Space.Self);
            if (transform.position.y < minZoom || transform.position.y > maxZoom) transform.position = initialPosition;
            initialPosition = transform.position;
        }
    }
}
