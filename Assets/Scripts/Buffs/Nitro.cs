using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Nitro : Buff
{
    [SerializeField]
    private int boostPower = 10000;

    public override void Apply(GameObject applyTo, int boost)
    {
        CmdIncreaseSpeed(applyTo, boost);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var colliderGameObject = other.gameObject;
        Apply(colliderGameObject, boostPower);
    }

    [Command]
    private void CmdIncreaseSpeed(GameObject applyTo, int boost)
    {
        var carMovement = applyTo.GetComponent<CarMovement>();
        if (!isServer)
        {
            return;
        }
        carMovement.IncreaseSpeed(boost);
    }
}
