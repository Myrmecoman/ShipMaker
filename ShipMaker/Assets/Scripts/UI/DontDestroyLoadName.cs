using UnityEngine;


public class DontDestroyLoadName : MonoBehaviour
{
    public string NameShip = "Default";

    private static DontDestroyLoadName _instance;


	// make this a singleton
	void Awake()
	{

		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
	}
}