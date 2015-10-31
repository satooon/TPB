using UnityEngine;
using System.Collections;

public class Character : Photon.MonoBehaviour {

	public enum Player {
		Unitychan,
	}

	public Camera MainCamera;
	public UnityChan.UnityChanControlScriptWithRgidBody UnityChan;

	private GameObject goBomb;

	// Use this for initialization
	void Start () {
		this.MainCamera.enabled = this.photonView.isMine;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (this.photonView.isMine) {
			this.UnityChan.FixedUpdateMine();
		}
	}

	public void BombOutput() {
		this.goBomb = Bomb.Type.Nomal.CreatePhotonInstance (Vector3.zero);
		this.goBomb.transform.parent = this.transform;
		this.goBomb.transform.localScale = new Vector3 (20f, 20f, 20f);
		this.goBomb.transform.localPosition = new Vector3(0.5f, 0.5f, 0f);
	}

	public void BombShoot() {
		if (this.goBomb == null) {
			return;
		}

		Vector3 center = new Vector3 (Screen.width / 2, Screen.height / 2, 0f);
		center.z += 25f; 
		center.x += 3f;
		Vector3 pos = Camera.main.ScreenToWorldPoint (center);
        
        this.goBomb.transform.parent = this.transform.parent;
		this.goBomb.transform.localPosition = pos;
		StartCoroutine (this.bombExplosion(this.goBomb));
	}

	private IEnumerator bombExplosion(GameObject bomb) {
		yield return new WaitForSeconds (2f);
		bomb.GetComponent<Bomb> ().BombExplosion ();
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
			Random.Range(0f, 0f),
			Random.Range(0f, 70f)
		);
		return value.CreatePhotonInstance (pos);
	}
}
