using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour {

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float yMin = 11f; //ograniczenie do offsetu
    public float yMax = 30f; //ograniczenie do offsetu

    public float radious = 7f;

    private Vector3 previousPos;

    [Range(5, 15)]
    public float scrollSpeed = 10f;

    public float defaultFOV = 70;
    public float zoomRatio = 0.5f;

    private Camera camera;
	// Use this for initialization
	void Start () {
        //if (!isLocalPlayer)
        //{
        //    Destroy(this);
        //}
        camera = Camera.main;
        previousPos = camera.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        camera.transform.LookAt(transform.position);
        offset.x = -radious * Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        offset.z = -radious * Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        Vector3 desiredPos = transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(previousPos, desiredPos, smoothSpeed);
        camera.transform.position = smoothedPosition;
        previousPos = camera.transform.position;
        
    }

    private void LateUpdate()
    {
        camera.fieldOfView = defaultFOV + GetComponent<Rigidbody>().velocity.magnitude * zoomRatio * Time.deltaTime;
    }
}
