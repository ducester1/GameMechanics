using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Rigidbody rb;
    public float maxSpeed;
    public float minSpeed;

    public float speed;

    public float ZoomAmount = 0f;
    public float MaxToClamp = 10f;
    public float zoomSpeed = 30f;

    public float minY = 10f;
    public float maxY = 80f;

    private float zoomingArea;
    private float minMaxSpeedDif;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        zoomingArea = maxY - minY;
        minMaxSpeedDif = maxSpeed - minSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //moving around
        speed = (transform.position.y - minY) / zoomingArea;
        speed = minMaxSpeedDif * speed;

        //if (speed >= -16) speed = -16;
        float vx = Input.GetAxis("Horizontal");
        float vz = Input.GetAxis("Vertical");
        Debug.Log("vx = " + vx + " vz = " + vz);
        rb.velocity = new Vector3(vx, 0, vz) * Mathf.Abs(speed);

        //zooming
        ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
        if (transform.position.y >= minY && transform.position.y <= maxY)
        {
            var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));
            gameObject.transform.Translate(0, 0, translate * zoomSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
        }
        else if (transform.position.y < minY) transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        else if (transform.position.y > maxY) transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
    }
}
