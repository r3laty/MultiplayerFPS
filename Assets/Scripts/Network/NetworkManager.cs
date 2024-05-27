using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    private void Start()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");

        RoomOptions options = new RoomOptions()
        {
            MaxPlayers = 5,
            IsVisible = true
        };

        PhotonNetwork.JoinOrCreateRoom("FPStest", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        int playerId = PhotonNetwork.LocalPlayer.ActorNumber;

        Debug.Log($"Joined room {PhotonNetwork.CurrentRoom.Name}" +
            $"\nID {playerId}");

        GameObject playerInstance = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        playerInstance.GetComponent<PlayerSetup>().IsLocalPlayer();
    }
}
