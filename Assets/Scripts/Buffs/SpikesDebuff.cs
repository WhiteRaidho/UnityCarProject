using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpikesDebuff : Buff
{
    [SerializeField]
    private int hpToBoost = -30;

    public override void Apply(GameObject applyTo, int booster)
    {
        var playerStats = applyTo.GetComponent<PlayerStats>();
        playerStats.CmdIncreasePoints(booster);
    }

    private void OnTriggerEnter(Collider other)
    {
        var colliderGameObject = other.gameObject;
        Apply(colliderGameObject, hpToBoost);
        Destroy(gameObject);
    }
}
