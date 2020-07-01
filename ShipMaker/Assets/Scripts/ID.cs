using UnityEngine;
using UnityEngine.SceneManagement;


public class ID : MonoBehaviour
{
    public uint Id = 0;


    void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Craft")
            GetComponent<Rigidbody>().isKinematic = true;
    }
}