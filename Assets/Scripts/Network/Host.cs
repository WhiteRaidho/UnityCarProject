using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Host : MonoBehaviour {
    public GameObject back;
    [SerializeField]
    private uint roomSize = 4;
    [SerializeField]
    private string roomName;

    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }
    public void SetRoomName(string _roomName)
    {
        roomName = _roomName;
    }

    public void CreateRoom()
    {
        if(roomName != null && roomName != "")
        {
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
            //Debug.Log("utworzono pokoj");
            back.SetActive(false);
        }
    }
}
