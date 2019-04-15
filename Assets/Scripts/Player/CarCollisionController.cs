using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CarCollisionController : NetworkBehaviour {
    [SerializeField]
    private AudioSource engine;
    private void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(GetComponent<CameraController>());
            Destroy(GetComponent<CarMovement>());
        }
    }

    private void Update()
    {
        engine.volume = 0.25f + Mathf.Clamp(GetComponent<CarController>().rb.velocity.magnitude / 50, 0, 0.25f);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            GetComponent<PlayerStats>().CmdIncreasePoints((int)Mathf.Abs(GetComponent<Rigidbody>().velocity.magnitude));
        }
    }
}