using UnityEngine;
using UnityEngine.SceneManagement;


public class Propeller : MonoBehaviour
{
    public float PowerMultiplier = 1;
    public bool Activated = true;
    public Transform propeller;
    [HideInInspector] public Rigidbody Boat;

    private bool rightScene;


    void Awake()
    {
        rightScene = SceneManager.GetActiveScene().name != "Craft";
    }


    void Update()
    {
        if (rightScene)
            propeller.localEulerAngles = new Vector3(propeller.localEulerAngles.x, propeller.localEulerAngles.y, propeller.localEulerAngles.z - Time.deltaTime * Boat.velocity.magnitude * 1000);
    }
}