using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour {
	
	void Awake() {
		// Server接続
		PhotonNetwork.ConnectUsingSettings("v0.1");
	}

	// GUI表示
	void OnGUI() {
		// Photon接続状態
		GUILayout.Label ("ConnectionStatus:" + PhotonNetwork.connectionStateDetailed.ToString());
		if (PhotonNetwork.inRoom) {
			GUILayout.Label ("RoomName:" + PhotonNetwork.room.name);
		}
	}

	public static void JoinOrCreateRoom () {
		PhotonNetwork.JoinOrCreateRoom ("test", new RoomOptions(){ isOpen = true, isVisible = true }, null);
	}
	
	// Lobby参加OK時
	public virtual void OnJoinedLobby() {
		Debug.Log ("Lobbby Join!");
	}
	
	// Room参加NG時
	public virtual void OnPhotonRandomJoinFailed() {
		Debug.Log("Room参加失敗！");
	}
	
	// Room参加OK時
	public virtual void OnJoinedRoom() {
		Debug.Log("Room参加成功！");
	}
}