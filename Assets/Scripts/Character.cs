using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public enum Player {
		Unitychan,
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
			Random.Range(100f -0.5f, 200f + 0.5f), 
			Random.Range(2f, 50f),
			Random.Range(100f - 0.5f, 200f + 0.50f)
		);
		return value.CreatePhotonInstance (pos);
	}
}
