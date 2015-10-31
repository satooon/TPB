using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoyStick : MonoBehaviour
{

	public static bool IsPush;
	public static float h;
	public static float v;

	private int touchId = -1;
	private Vector3 firstPos;

	private float maximum = 60f;


	// Use this for initialization
	void Start () 
	{
		IsPush = false;
	}

	private Vector3 _nowPos = Vector3.zero; 
	private Vector3 _diffPos = Vector3.zero; 
	// Update is called once per frame
	void Update () 
	{
		if (IsPush) {
#if UNITY_EDITOR
			_nowPos = Input.mousePosition;
#else
			if (Input.touchCount > 0){
				for (int i = 0; i < Input.touchCount; i++) {
					if (Input.touches[i].fingerId == touchId) {
						_nowPos = Input.touches[i].position;
					}
				}
			}
#endif
			_diffPos = firstPos - _nowPos;
			if (Mathf.Abs(_diffPos.x) > 20f) {
				if (_diffPos.x > 0) {
					h = _diffPos.x / (maximum*5f) > 1f ? -1f : -(_diffPos.x / (maximum*5f));
				} else {
					h = _diffPos.x / (maximum*5f) < -1f ? 1f : -(_diffPos.x / (maximum*5f));
				}
			} else {
				h = 0f;
			}
			if (Mathf.Abs(_diffPos.y) > 20f) {
				if (_diffPos.y > 0) {
					v = _diffPos.y / (maximum*5f) > 1f ? -1f : -(_diffPos.y / (maximum*5f));
				} else {
					v = _diffPos.y / (maximum*5f) < -1f ? 1f : -(_diffPos.y / (maximum*5f));
				}
			} else {
				v = 0f;
			}
		}
	}
		
	public void OnPushDown()
	{
		if (IsPush) {
			return;
		}
		IsPush = true;
		Vector3 pos = Vector3.zero;
#if UNITY_EDITOR
		pos = Input.mousePosition;
#else
		if (Input.touchCount > 0){
			if(Input.touches[0].phase == TouchPhase.Began){
				pos = Input.touches[0].position;
				touchId = Input.touches[0].fingerId;
			}
		}
#endif

		firstPos = pos;
	}
	
	public void OnPushUp()
	{
		_diffPos = Vector3.zero;
		h = 0f;
		v = 0f;
		touchId = -1;
		IsPush = false;
	}

/*
	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		style.richText = true;
		GUI.Label ( new Rect(0, Screen.height - 100, 300, 100), "<size=30>X:" + _diffPos.x + " Y:" + _diffPos.y + "</size>", style);
	}
	*/
}
