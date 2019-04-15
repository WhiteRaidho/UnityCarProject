using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class CarController : MonoBehaviour {

    public Rigidbody rb;
    //public float rpm = 200;

    [Header("Car stats(not used)")]
    public float wheelR;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
    }

    void Update () {
        
	}

}