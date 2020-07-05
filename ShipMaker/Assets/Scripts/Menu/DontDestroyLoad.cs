using UnityEngine;


public class DontDestroyLoad : MonoBehaviour
{
    public string fileValue;


    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}