using UnityEngine;


public class DontDestroyLoadName : MonoBehaviour
{
    public string Name = "Default";


    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}