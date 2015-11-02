using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleController : NetworkManager {

	public static int TimerLimit = 60;

	public Text TextPoint;
	public Text TextName;
	public Text TextTimer;
	public Text TextGameOver;
	public Text TextRecord;

	private int currentTime;
	private Character currentCharacter;
	private int tmpPoint;

	private Dictionary<int, List<string>> recordDictionary;

	// Use this for initialization
	void Start () {
		this.TextTimer.text = BattleController.TimerLimit + "s";
		this.recordDictionary = new Dictionary<int, List<string>> ();
		this.currentTime = BattleController.TimerLimit;

		if (PhotonNetwork.inRoom) {
			this.play();
        }
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

		PhotonNetwork.player.name = "Player" + PhotonNetwork.room.playerCount;

		this.play ();
	}

	private void play() {
		//プレイヤーをインスタンス化
		this.currentCharacter = Character.Player.Unitychan.CreatePhotonInstance ().GetComponent<Character>();
		this.currentCharacter.BattleController = this;
		
		StartCoroutine (this.startTimer());
	}

	/// <summary>
	/// 接続後にPhotonサーバーとの通信が失敗した場合に呼ばれます。
	/// </summary>
	/// <param name='cause'>
	/// 原因
	/// </param>
	public virtual void OnConnectionFail (DisconnectCause cause) {
		Debug.Log (cause);
	}
	
	/// <summary>
	/// Photonサーバーに繋げなかった場合に呼ばれます。
	/// </summary>
	/// <param name='cause'>
	/// 原因
	/// </param>
	public virtual void OnFailedToConnectToPhoton (DisconnectCause cause) {
		Debug.Log (cause);
	}

	public void PushDown() {
		Debug.Log ("PushDown");

		this.currentCharacter.BombOutput ();
	}
	
	public void PushUp() {
		Debug.Log ("PushUp");

		this.currentCharacter.BombShoot ();
	}

	private IEnumerator startTimer() {
		while (this.currentTime > 0) {
			yield return new WaitForSeconds(1.0f);
			this.currentTime--;
			this.TextTimer.text = this.currentTime + "s";
		}

		StartCoroutine (this.gameOver());
	}

	private IEnumerator gameOver() {
		Debug.Log ("Game Over");

		this.tmpPoint = this.currentCharacter.Point;

		PhotonNetwork.DestroyAll ();
		this.TextGameOver.gameObject.SetActive (true);

		if (!this.recordDictionary.ContainsKey(this.tmpPoint)) {
			this.recordDictionary [this.tmpPoint] = new List<string>();
		}
		this.recordDictionary [this.tmpPoint].Add (PhotonNetwork.player.name);
		this.SendRecord ();

		yield return new WaitForSeconds (2.0f);

		StartCoroutine (this.result());
	}

	public void SendRecord() {
		this.photonView.RPC (
			"ReceiveRecord",
			PhotonTargets.Others,
			new object[]{PhotonNetwork.player.name, this.tmpPoint}
		);
	}

	[PunRPC]
	public void ReceiveRecord(string name, int point) {
		if (!this.recordDictionary.ContainsKey(this.tmpPoint)) {
			this.recordDictionary[point] = new List<string>();
		}
		this.recordDictionary [point].Add (name);
	}

	private IEnumerator result() {
		int maxPoint = 0;
		foreach (var key in this.recordDictionary.Keys) {
			maxPoint = Mathf.Max(maxPoint, key);
		}
		List<string> nameList = this.recordDictionary[maxPoint];

		this.TextGameOver.gameObject.SetActive (false);
		this.TextRecord.gameObject.SetActive (true);
		this.TextRecord.text = "うぃな〜\n" + string.Join("\n", nameList.ToArray());

		yield return new WaitForSeconds (5.0f);
		FadeManager.Instance.LoadLevel (Common.Scene.Title.GetSceneName(), Common.Scene.Title.GetFadeDuration());
	}
}
