using UnityEngine;


public class CraftCam : MonoBehaviour
{
    public Transform Cam;
    public float Sensivity;

    public GameObject Hull;

    private float CamRotVal;

    // Inputs
    [HideInInspector] public Vector2 WantMove = Vector2.zero;
    [HideInInspector] public Vector2 WantMouse = Vector2.zero;


    #region Input Functions

    public void PressEscape()
    {
        Debug.Log("escape");
    }


    public void PressClick()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 30))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 3);
            Instantiate(Hull, hit.collider.transform.position + hit.normal, Quaternion.identity);
            // check if there is already something
        }
    }

    #endregion


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        // rotation
        CamRotVal = Mathf.Clamp(CamRotVal - WantMouse.y * Sensivity, -89, 89);
        Cam.eulerAngles = new Vector3(360 + CamRotVal, Cam.eulerAngles.y + WantMouse.x * Sensivity, 0);

        // movements

    }
}