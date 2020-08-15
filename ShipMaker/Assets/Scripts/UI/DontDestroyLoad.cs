using UnityEngine;


public class DontDestroyLoad : MonoBehaviour
{
    public string fileValue;

    private static DontDestroyLoad _instance;


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