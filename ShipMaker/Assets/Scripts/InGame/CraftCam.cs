using UnityEngine;
using UnityEngine.UI;


public class CraftCam : MonoBehaviour
{
    public Transform Cam;
    public float Sensivity;
    public Text ShipName;
    public Button Save;

    public GameObject Hull;

    private float CamRotVal;
    private float speed = 5;
    private GameObject GravitationCenter = null;
    private bool LockMove = false;
    private SaveLoad saveNload;

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
        LockMove = !LockMove;
    }


    public void PressClick()
    {
        if (LockMove)
            return;

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


    public void ExitToMenu()
    {

    }


    public void SaveAs()
    {
        if (ShipName.text == "")
        {
            Debug.Log("can't save, name is empty");
            return;
        }

        ID[] allPieces = GetComponents<ID>();
        Debug.Log(allPieces.Length);
        string strs = "";
        foreach (ID piece in allPieces)
        {
            strs +=
                piece.Id + "-" +
                piece.transform.position.x + ":" +
                piece.transform.position.y + ":" +
                piece.transform.position.z + "-" +
                piece.transform.rotation.x + ":" +
                piece.transform.rotation.y + ":" +
                piece.transform.rotation.z + "\n";
        }
        Debug.Log(strs);
        saveNload.SaveAs(ShipName.text, strs);
    }

    #endregion


    void Start()
    {
        saveNload = new SaveLoad();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (LockMove)
            return;

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