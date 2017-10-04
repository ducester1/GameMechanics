using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    Rigidbody rb;
    public int speed = 10;
    float ZoomAmount = 0;
    float MaxToClamp = 10;
    public float ROTSpeed = 10;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //heen en weer bewegen
        float vx = Input.GetAxis("Horizontal");
        float vz = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(vx, 0, vz) * speed;

        ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
        var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));
        gameObject.transform.Translate(0, 0, translate * ROTSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
    }
}
