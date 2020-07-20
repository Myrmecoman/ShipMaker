using UnityEngine;


public class FPSdisplay : MonoBehaviour
{
	float deltaTime = 0.0f;

	private static FPSdisplay _instance;


	// make this a singleton
	void Awake()
	{

		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
	}


	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}


	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
		float fps = 1.0f / deltaTime;
		string text = string.Format(" {0:0.} fps", fps);
		GUI.Label(rect, text, style);
	}
}