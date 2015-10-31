using UnityEngine;
using System.Collections;

public class Character : Photon.MonoBehaviour {

	public enum Player {
		Unitychan,
	}

	public Camera MainCamera;
	public UnityChan.UnityChanControlScriptWithRgidBody UnityChan;

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
			Random.Range(0f, 00f),
			Random.Range(0f, 70f)
		);
		return value.CreatePhotonInstance (pos);
	}
}
