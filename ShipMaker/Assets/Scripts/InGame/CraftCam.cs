using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CraftCam : MonoBehaviour
{
    public Transform Cam;
    public float Sensivity;
    public Text ShipName;
    public GameObject DontDestroyToTest;
    public LayerMask layermask;
    public Image Cross;
    public Sprite CrossBlack;
    public Sprite CrossRed;
    public Image SelectedElement;

    private float CamRotVal;
    private float speed = 5;
    private GameObject GravitationCenter = null;
    private bool LockMove = false;
    private SaveLoad saveNload;
    private string fileValueStored;
    private string PrefixStr;

    // Inputs
    [HideInInspector] public Vector2 WantMove = Vector2.zero;
    [HideInInspector] public Vector2 WantMouse = Vector2.zero;
    [HideInInspector] public Vector2 WantScroll = Vector2.zero;
    [HideInInspector] public bool RightClickHold;
    [HideInInspector] public bool AltHold;
    [HideInInspector] public string SelectedID = "1";


    #region Input Functions

    public void PressEscape()
    {
        // cursor usual things
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        LockMove = !LockMove;
    }


    public void PressClick()
    {
        if (LockMove)
            return;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 30, layermask))
        {
            // Add or remove
            if (AltHold && hit.collider.GetComponent<ID>().Id != 0)
                Destroy(hit.collider.gameObject);
            if(!AltHold)
            {
                // check if there is already something
                Collider[] hitColliders = Physics.OverlapSphere(hit.collider.transform.position + hit.normal, 0.4f);
                if (hitColliders.Length == 0)
                {
                    UpdatePrefix();
                    Instantiate(Resources.Load("Craft/" + PrefixStr + SelectedID, typeof(GameObject)), hit.collider.transform.position + hit.normal, Quaternion.identity);
                }
            }
        }
    }


    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menus");
    }


    public void SaveAs()
    {
        if (ShipName.text == "")
            return;

        ID[] allPieces = FindObjectsOfType<ID>();
        string strs = ShipName.text + '\n';
        foreach (ID piece in allPieces)
        {
            strs +=
                piece.Id + "_" +
                piece.transform.position.x + ":" +
                piece.transform.position.y + ":" +
                piece.transform.position.z + "_" +
                piece.transform.rotation.x + ":" +
                piece.transform.rotation.y + ":" +
                piece.transform.rotation.z + "\n";
        }
        saveNload.SaveAs(ShipName.text, strs);
    }


    public void SaveNquit()
    {
        SaveAs();
        ExitToMenu();
    }


    public void Test()
    {
        if (ShipName.text == "")
            return;

        SaveAs();
        GameObject value = Instantiate(DontDestroyToTest);
        value.GetComponent<DontDestroyLoad>().fileValue = saveNload.LoadAs(ShipName.text);
        SceneManager.LoadScene("TestScene");
    }

    #endregion


    void Awake()
    {
        SelectedID = "1";
        PrefixStr = "Cubes/";

        DontDestroyLoad obj = FindObjectOfType<DontDestroyLoad>();
        if (obj)
        {
            fileValueStored = obj.fileValue;
            Destroy(obj.gameObject);
            StringReader reader = new StringReader(fileValueStored);
            ShipName.text = reader.ReadLine();
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;

                ushort nb1 = 0;
                ushort nb2 = 0;
                string id = "";
                string posx = "";
                string posy = "";
                string posz = "";
                string rotx = "";
                string roty = "";
                string rotz = "";

                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == '_')
                    {
                        nb1++;
                        nb2 = 0;
                        continue;
                    }
                    if (line[j] == ':')
                    {
                        nb2++;
                        continue;
                    }

                    // get id
                    if (nb1 == 0)
                        id += line[j];

                    // get positions
                    if (nb1 == 1 && nb2 == 0)
                        posx += line[j];
                    if (nb1 == 1 && nb2 == 1)
                        posy += line[j];
                    if (nb1 == 1 && nb2 == 2)
                        posz += line[j];

                    // get rotations
                    if (nb1 == 2 && nb2 == 0)
                        rotx += line[j];
                    if (nb1 == 2 && nb2 == 1)
                        roty += line[j];
                    if (nb1 == 2 && nb2 == 2)
                        rotz += line[j];
                }
                SelectedID = id;
                UpdatePrefix();
                Instantiate(
                    Resources.Load("Craft/" + PrefixStr + id, typeof(GameObject)),
                    new Vector3(float.Parse(posx), float.Parse(posy), float.Parse(posz)),
                    Quaternion.Euler(float.Parse(rotx), float.Parse(roty), float.Parse(rotz)));
            }
        }
        else
        {
            DontDestroyLoadName newship = FindObjectOfType<DontDestroyLoadName>();
            ShipName.text = newship.Name;
            Destroy(newship.gameObject);
            Instantiate(Resources.Load("Craft/Cubes/0", typeof(GameObject)), Vector3.zero, Quaternion.identity);
        }
    }


    private void UpdatePrefix()
    {
        if (int.Parse(SelectedID) < 200)
        {
            PrefixStr = "Cubes/";
            return;
        }
        if (int.Parse(SelectedID) < 400)
        {
            PrefixStr = "Weapons/";
            return;
        }
        if (int.Parse(SelectedID) < 600)
        {
            PrefixStr = "Engines/";
            return;
        }
        if (int.Parse(SelectedID) < 800)
        {
            PrefixStr = "Cosmetics/";
            return;
        }
    }


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

        if (AltHold && Cross.sprite != CrossRed)
            Cross.sprite = CrossRed;
        else if(!AltHold && Cross.sprite != CrossBlack)
            Cross.sprite = CrossBlack;

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