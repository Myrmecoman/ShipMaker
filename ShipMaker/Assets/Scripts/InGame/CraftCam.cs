using System.Collections.Generic;
using UnityEngine;


public class CraftCam : MonoBehaviour
{
    public Transform Cam;
    public float Sensivity;

    public GameObject Hull;

    private float CamRotVal;
    private float speed = 5;
    private GameObject GravitationCenter = null;
    private List<Vector3> LoadedShip = new List<Vector3>();

    // Inputs
    [HideInInspector] public Vector2 WantMove = Vector2.zero;
    [HideInInspector] public Vector2 WantMouse = Vector2.zero;
    [HideInInspector] public Vector2 WantScroll = Vector2.zero;
    [HideInInspector] public bool RightClickHold;


    #region Input Functions

    public void PressEscape()
    {
        // cursor usual things
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        Cursor.visible = Cursor.lockState == CursorLockMode.None;
    }


    public void PressClick()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 30))
        {
            // check if there is already something
            Collider[] hitColliders = Physics.OverlapSphere(hit.collider.transform.position + hit.normal, 0.4f);
            if (hitColliders.Length == 0)
            {
                Instantiate(Resources.Load("Craft/Cubes/Hull", typeof(GameObject)), hit.collider.transform.position + hit.normal, Quaternion.identity);
            }
        }
    }

    #endregion


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        // rotation and movements
        if (!GravitationCenter)
        {
            CamRotVal = Mathf.Clamp(CamRotVal - WantMouse.y * Sensivity, -89, 89);
            Cam.eulerAngles = new Vector3(360 + CamRotVal, Cam.eulerAngles.y + WantMouse.x * Sensivity, 0);

            Cam.position += transform.forward * WantMove.y * Time.deltaTime * speed + transform.right * WantMove.x * Time.deltaTime * speed;
        }
        else
        {
            GravitationCenter.transform.eulerAngles += new Vector3(GravitationCenter.transform.eulerAngles.x, WantMouse.x * Sensivity, 0);
        }

        // change speed
        speed = Mathf.Clamp(speed + WantScroll.y * 1 / 120, 1, 40);

        // when right clicking, rotate around a point
        if(RightClickHold)
        {
            if(!GravitationCenter && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 20))
            {
                GravitationCenter = new GameObject();
                GravitationCenter.transform.position = hit.point;
                GravitationCenter.transform.eulerAngles = Vector3.zero;
                transform.parent = GravitationCenter.transform;
            }
            else if(!GravitationCenter)
            {
                GravitationCenter = new GameObject();
                GravitationCenter.transform.position = transform.position + transform.TransformDirection(Vector3.forward) * 20;
                GravitationCenter.transform.eulerAngles = Vector3.zero;
                transform.parent = GravitationCenter.transform;
            }
        }
        else if(GravitationCenter)
        {
            transform.parent = null;
            Destroy(GravitationCenter);
        }
    }
}