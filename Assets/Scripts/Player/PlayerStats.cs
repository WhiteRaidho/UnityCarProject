using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour {

    [SyncVar (hook="OnPointsChanged")]
    [SerializeField]
    private int currentPoints;

    private Text currentPointsText;

    [SerializeField]
    private int pointsToWin = 5;

    private void Awake()
    {
        currentPoints = 0;
    }

    private void Update()
    {
        if(currentPoints > 500)
        {
            GetComponent<CarMovement>().canMove = false;
            //GetComponent<Rigidbody>().AddExplosionForce(50000, new Vector3(transform.position.x, 0, transform.position.z), 20);
        }
    }

    private void Start()
    {
        currentPointsText = GameObject.Find("Points Text").GetComponent<Text>();
        SetCurrentPointsText();
    }

    public int GetCurrentPoints()
    {
        return currentPoints;
    }

    [Command]
    public void CmdIncreasePoints(int points) {
        if (!isServer)
        {
            return;
        }
        currentPoints += points;
    }
    
    private void SetCurrentPointsText()
    {
        if (isLocalPlayer)
        {
            currentPointsText.text = "Points: " + currentPoints.ToString();
        }
    }

    private void OnPointsChanged(int newPointsAmount)
    {
        currentPoints = newPointsAmount;

        SetCurrentPointsText();
    }
}
