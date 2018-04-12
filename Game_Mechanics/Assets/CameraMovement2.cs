using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    public float scrollSpeed;
    public float rotationSpeed;
    public float panSpeed;
    public float wasdSpeed;
    public float minZoom;
    public float maxZoom;
    public float panBorderThickness;

    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private Vector3 initialMousePosition;
    private Vector3 wasdPos;
    private Vector3 initialTransform;
    private bool isPanning;
    private float yRotation;
    private float xRotation;
    private bool isRotating;





    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.localEulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isPanning = true;
        }
        if (Input.GetMouseButtonDown(2))
        {
            isRotating = true;
        }

        //move camera on X & Y with WASD or bij panning at the side of the screen
        wasdPos = transform.position;
        initialTransform = transform.localEulerAngles;
        transform.eulerAngles = new Vector3(0, initialTransform.y, initialTransform.z);
        if ((Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - panBorderThickness && !isRotating)) && !isPanning)
        {
            wasdPos += transform.forward * wasdSpeed * Time.deltaTime;
        }
        if ((Input.GetKey("s") || (Input.mousePosition.y <= panBorderThickness && !isRotating)) && !isPanning)
        {
            wasdPos += -transform.forward * wasdSpeed * Time.deltaTime;
        }
        if ((Input.GetKey("a") || (Input.mousePosition.x <= panBorderThickness && !isRotating)) && !isPanning)
        {
            wasdPos += -transform.right * wasdSpeed * Time.deltaTime;
        }
        if ((Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - panBorderThickness && !isRotating)) && !isPanning)
        {
            wasdPos += transform.right * wasdSpeed * Time.deltaTime;
        }
        if (wasdPos != transform.position)
        {
            wasdPos.y = transform.position.y;
            transform.localPosition = wasdPos;
        }
        transform.eulerAngles = initialTransform;

        //cancel on button release

        if (!Input.GetMouseButton(2))
        {
            isRotating = false;
        }
        if (!Input.GetMouseButton(1))
        {
            isPanning = false;
        }
        //move camera on X & Y with mouse
        else if (isPanning && Input.mousePosition != initialMousePosition)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - initialMousePosition);
            Vector3 move = new Vector3(pos.x * -panSpeed, pos.y * -panSpeed, 0);
            Camera.main.transform.Translate(move, Space.Self);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            transform.localPosition = new Vector3(transform.position.x, transform.position.y - (transform.position.y - (initialPosition.y)), transform.position.z);
        }
        //rotate camera with mousewheel click
        if (Input.GetMouseButton(2))
        {
            yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            yRotation = Mathf.Clamp(yRotation, -60, 30);
            xRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            xRotation = xRotation % 360;
            transform.localEulerAngles = new Vector3(yRotation + initialRotation.x, xRotation + initialRotation.y, 0);
        }
        //zoom with scroll
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && transform.position.y >= minZoom && transform.position.y <= maxZoom)
        {
            initialPosition = transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed * Input.GetAxis("Mouse ScrollWheel"), Space.Self);
            if (transform.position.y < minZoom || transform.position.y > maxZoom) transform.position = initialPosition;
            initialPosition = transform.position;
        }

        initialMousePosition = Input.mousePosition;
    }
}
