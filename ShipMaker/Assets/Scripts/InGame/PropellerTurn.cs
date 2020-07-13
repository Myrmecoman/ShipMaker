using UnityEngine;
using UnityEngine.SceneManagement;


public class PropellerTurn : MonoBehaviour
{
    private Rigidbody Boat;
    private bool rightScene;


    void Awake()
    {
        rightScene = SceneManager.GetActiveScene().name != "Craft";
        if(rightScene)
            Boat = transform.parent.GetComponentInParent<Rigidbody>();
    }


    void Update()
    {
        if (rightScene)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - Time.deltaTime * Boat.velocity.magnitude * 1000);
    }
}