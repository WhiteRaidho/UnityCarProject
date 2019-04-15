using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PointsBooster : Buff {
    [SerializeField]
    private int pointsToAdd = 100;

    public override void Apply(GameObject applyTo, int booster)
    {
        var playerStats = applyTo.GetComponent<PlayerStats>();
        playerStats.CmdIncreasePoints(booster);
    }

    private void OnTriggerEnter(Collider other)
    {
        var colliderGameObject = other.gameObject;
        Apply(colliderGameObject, pointsToAdd);
        Destroy(gameObject);
    }
}
