using UnityEngine;
using System.Collections;

public class Character : Photon.MonoBehaviour {

	public enum Player {
		Unitychan,
	}
	public static int BombCountMax = 3;

	public Camera MainCamera;
	public UnityChan.UnityChanControlScriptWithRgidBody UnityChan;

	private GameObject goBomb;
	private int bombCount;

	// Use this for initialization
	void Start () {
		this.MainCamera.enabled = this.photonView.isMine;
		this.bombCount = Character.BombCountMax;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (this.photonView.isMine) {
			this.UnityChan.FixedUpdateMine();
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag(Bomb.TagName)) {
			Debug.Log("Dead");
		}
	}

	public void BombOutput() {
		if (this.bombCount <= 0) {
			return;
		}
		this.bombCount--;

		this.goBomb = Bomb.Type.Nomal.CreatePhotonInstance (Vector3.zero);
		this.goBomb.transform.parent = this.transform;
		this.goBomb.transform.localScale = new Vector3 (20f, 20f, 20f);
		this.goBomb.transform.localPosition = new Vector3(0.5f, 0.5f, 0f);
	}

	public void BombShoot() {
		if (this.goBomb == null) {
			return;
		}
		GameObject go = this.goBomb;
		this.goBomb = null;

		Vector3 center = new Vector3 (Screen.width / 2, Screen.height / 2, 0f);
		center.z += 25f; 
		center.x += 3f;
		Vector3 pos = Camera.main.ScreenToWorldPoint (center);
        
		go.transform.parent = this.transform.parent;
		go.transform.localPosition = pos;
		StartCoroutine (go.GetComponent<Bomb>().BombExplosion(() => {
			this.bombCount++;
		}));
	}
}

public static class CharacterPlayerExtension {

	private static string prefabPath = "Prefabs/Chara/{0}";
	private static string[] playerPrefabName = new string[]{
		"PrefabUnitychan",
	};

	public static int GetIntValue(this Character.Player value) {
		return (int)value;
	}
	
	public static string GetPrefabPath(this Character.Player value) {
		return string.Format (CharacterPlayerExtension.prefabPath, CharacterPlayerExtension.playerPrefabName[value.GetIntValue()]);
	}

	public static GameObject CreatePhotonInstance(this Character.Player value, Vector3 pos) {
		return PhotonNetwork.Instantiate(
			value.GetPrefabPath(),
			pos,
			Quaternion.identity,
			0
		);
	}

	public static GameObject CreatePhotonInstance(this Character.Player value) {
		Vector3 pos = new Vector3(
			Random.Range(-36f, 36f), 
			15f,
			Random.Range(0f, 70f)
		);
		return value.CreatePhotonInstance (pos);
	}
}
