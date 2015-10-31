using UnityEngine;
using System.Collections;

public class BattleController : NetworkManager {

	private Character currentCharacter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Lobby参加OK時
	void OnJoinedLobby() {
		base.OnJoinedLobby ();

		NetworkManager.JoinOrCreateRoom ();
	}
	
	// Room参加NG時
	void OnPhotonRandomJoinFailed() {
		base.OnPhotonRandomJoinFailed ();
	}
	
	// Room参加OK時
	void OnJoinedRoom() {
		base.OnJoinedRoom ();

		//プレイヤーをインスタンス化
		this.currentCharacter = Character.Player.Unitychan.CreatePhotonInstance ().GetComponent<Character>();
	}

	public void PushDown() {
		Debug.Log ("PushDown");

		this.currentCharacter.BombOutput ();
	}
	
	public void PushUp() {
		Debug.Log ("PushUp");

		this.currentCharacter.BombShoot ();
	}
}
