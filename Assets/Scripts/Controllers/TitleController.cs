using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTouch() {
//		FadeManager.Instance.LoadLevel (Common.Scene.CharacterSelect.GetSceneName(), Common.Scene.CharacterSelect.GetFadeDuration());
		FadeManager.Instance.LoadLevel (Common.Scene.Battle.GetSceneName(), Common.Scene.Battle.GetFadeDuration());
	}
}
