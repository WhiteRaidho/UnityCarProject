using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;


public class Join : MonoBehaviour {
    List<GameObject> roomList = new List<GameObject>();

    public GameObject back;

    private NetworkManager networkManager;

    [SerializeField]
    private GameObject listItemPrefab;

    [SerializeField]
    private Transform listItemParent;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
        RefreshRoomList();
	}
	
	// Update is called once per frame
	public void RefreshRoomList()
    {
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if(matchList == null)
        {
            return;
        }
        ClearRoomList();
        foreach(MatchInfoSnapshot match in matchList)
        {
            GameObject _roomListItemGO = Instantiate(listItemPrefab);
            _roomListItemGO.transform.SetParent(listItemParent);
            roomList.Add(_roomListItemGO);
            RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
            if(_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }
        }
        if(roomList.Count == 0)
        {

        }
    }

    void ClearRoomList()
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }
    public void JoinRoom(MatchInfoSnapshot _match)
    {
        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        back.SetActive(false);
    }
}
