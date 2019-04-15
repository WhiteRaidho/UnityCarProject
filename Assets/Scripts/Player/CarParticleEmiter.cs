using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarParticleEmiter : MonoBehaviour {

    public List<ParticleSystem> particleSystem;
    CarController con;

    // Use this for initialization
    void Start () {
        con = GetComponent<CarController>();
	}
	
	// Update is called once per frame
	void Update () {
        foreach(ParticleSystem ps in particleSystem)
        {
            ps.emissionRate = con.rb.velocity.sqrMagnitude/30;
        }
	}
}
