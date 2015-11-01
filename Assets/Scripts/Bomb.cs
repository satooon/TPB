using UnityEngine;
using System;
using System.Collections;

public class Bomb : Photon.MonoBehaviour {

	public enum Type {
		Nomal,
	}

	public static string TagName = "BombExplosion";

	public GameObject GoBomb;
	public GameObject GoExplosion;
	public Animator Animator;
	public Character Character { get; set; }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnGUI() {
//		GUILayout.Label ("Bomb x:" + this.transform.localPosition.x);
//		GUILayout.Label ("Bomb y:" + this.transform.localPosition.y);
//		GUILayout.Label ("Bomb z:" + this.transform.localPosition.z);
//	}
    
    public IEnumerator BombExplosion(Action callBack) {
		yield return new WaitForSeconds (2f);
		this.tag = Bomb.TagName;
		this.Animator.Play ("BombExplosion");
		if (callBack != null) {
			callBack();
		}
	}

//	public void BombShoot() {
//		Debug.Log ("BombShoot");
//		StartCoroutine (this.bombShoot());
//    }
//
//	private IEnumerator bombShoot() {
//		for (float i = 0.01f; i <= 0.2f; i+=0.01f) {
//			this.GoBomb.transform.localPosition = new Vector3(
//				this.GoBomb.transform.localPosition.x,
//				this.GoBomb.transform.localPosition.y,
//				this.GoBomb.transform.localPosition.z + i
//			);
//			yield return new WaitForFixedUpdate();
//		}
//
//		this.OnBombShootComplete ();
//	}
//
//	public void OnBombShootComplete() {
//		Debug.Log ("OnBombShootComplete");
//		this.Animator.Play ("BombExplosion");
//	}
//
	public void OnBombExplosionComplete() {
		Debug.Log ("OnBombExplosionComplete");
		PhotonNetwork.Destroy (this.gameObject);
	}
}

public static class BombTypeExtension {

	private static string prefabPath = "Prefabs/Bomb/{0}";
	private static string[] prefabName = new string[]{
		"PrefabBombNormal",
	};
	
	public static int GetIntValue(this Bomb.Type value) {
		return (int)value;
	}

	public static string GetPrefabPath(this Bomb.Type value) {
		return string.Format (BombTypeExtension.prefabPath, BombTypeExtension.prefabName[value.GetIntValue()]);
	}

	public static GameObject CreatePhotonInstance(this Bomb.Type value, Vector3 pos) {
		return PhotonNetwork.Instantiate(
			value.GetPrefabPath(),
			pos,
			Quaternion.identity,
			0
			);
	}
}
