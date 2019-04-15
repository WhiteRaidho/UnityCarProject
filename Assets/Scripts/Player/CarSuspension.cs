using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class CarSuspension : MonoBehaviour {

    public   Rigidbody rb;
    private CarController car;

    public float springForce;
    public float damperForce;
    public float springConstant;
    public float damperConstant;
    public float restLength;

    private float previousLength;
    private float currentLength;
    private float springVelocity;

	// Use this for initialization
	void Start () {
        //if (!isLocalPlayer)
        //{
        //    Destroy(this);
        //}
        car = GetComponentInParent<CarController>();
        rb = car.rb;
		
	}
	
	void FixedUpdate () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, restLength + car.wheelR))
        {
            previousLength = currentLength;
            currentLength = restLength - (hit.distance - car.wheelR);
            springVelocity = (currentLength - previousLength) / Time.fixedDeltaTime;
            springForce = springConstant * currentLength;
            damperForce = damperConstant * springVelocity;

            rb.AddForceAtPosition(transform.up * (springForce + damperForce), transform.position);
        }
	}
}
