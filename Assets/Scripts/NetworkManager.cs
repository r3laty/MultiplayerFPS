using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        RoomOptions options = new RoomOptions()
        {
            MaxPlayers = 3,
            IsVisible = true
        };

        PhotonNetwork.JoinOrCreateRoom("FPStest", options, TypedLobby.Default);

        Debug.Log($"We are connected to room");
    }
}
