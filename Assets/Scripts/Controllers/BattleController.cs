using UnityEngine;
using System.Collections;

public class BattleController : NetworkManager {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Lobby参加OK時
	void OnJoinedLobby() {
		base.OnJoinedLobby ();

		PhotonNetwork.CreateRoom (null);
	}
	
	// Room参加NG時
	void OnPhotonRandomJoinFailed() {
		base.OnPhotonRandomJoinFailed ();
	}
	
	// Room参加OK時
	void OnJoinedRoom() {
		base.OnJoinedRoom ();

		//プレイヤーをインスタンス化
		Character.Player.Unitychan.CreatePhotonInstance ();
	}
}
