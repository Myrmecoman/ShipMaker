using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CraftCam : MonoBehaviour
{
    public Transform Cam;
    public float Sensivity;
    public Text ShipName;
    public Text VolumeText;
    public ColorPicker colorPicker;
    public GameObject DontDestroyToTest;
    public LayerMask layermask;
    public Image Cross;
    public Sprite CrossBlack;
    public Sprite CrossRed;
    public GizmoAngle gizmoScript;
    public GameObject ColliderDisplay;

    private GameObject currentCol;
    private float CamRotVal;
    private float speed = 5;
    private GameObject GravitationCenter = null;
    private bool LockMove = false;
    private SaveLoad saveNload;
    private string fileValueStored;
    private string PrefixStr;
    [HideInInspector] public string SelectedID = "0";
    [HideInInspector] public uint TotalVolume = 0;
    [HideInInspector] public uint TotalParts = 0;

    // Inputs
    [HideInInspector] public Vector2 WantMove = Vector2.zero;
    [HideInInspector] public Vector2 WantMouse = Vector2.zero;
    [HideInInspector] public Vector2 WantScroll = Vector2.zero;
    [HideInInspector] public bool RightClickHold;
    [HideInInspector] public bool LeftClickHold;
    [HideInInspector] public bool AltHold;


    #region Input Functions

    public void PressEscape()
    {
        // cursor usual things
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (colorPicker.BuildMode.text == "Paint mode")
            {
                colorPicker.BuildMode.text = "Craft mode";
                return;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            LockMove = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            LockMove = false;
        }
    }


    public void PressClick()
    {
        if (LockMove)
            return;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 30, layermask))
        {
            // craft mode
            if (colorPicker.BuildMode.text == "Craft mode")
            {
                // remove
                if (AltHold && TotalParts > 1)
                {
                    GameObject destroyed = hit.collider.gameObject;
                    Destroy(destroyed);
                    TotalVolume -= destroyed.GetComponent<ID>().Volume;
                    VolumeText.text = "Total volume : " + TotalVolume;
                    TotalParts--;
                }
                // add
                if (!AltHold)
                {
                    // check if there is already something, and if allowed to build here
                    Collider[] hitColliders = Physics.OverlapSphere(hit.collider.transform.position + hit.normal, 0.4f);

                    Transform[] simp = hit.collider.gameObject.GetComponent<ID>().CanBuild;
                    float miniDistance = 999;
                    int miniDistanceIndex = 0;
                    for (int i = 0; i < simp.Length; i++)
                    {
                        float dist = Vector3.Distance(simp[i].position, hit.collider.transform.position + hit.normal);
                        if (dist < miniDistance)
                        {
                            miniDistance = dist;
                            miniDistanceIndex = i;
                        }
                    }

                    if (hitColliders.Length == 0 && simp[miniDistanceIndex].name != "no")
                    {
                        UpdatePrefix();
                        Quaternion rot = Quaternion.identity;
                        rot.eulerAngles = gizmoScript.rotation;
                        GameObject instantiated = Instantiate(Resources.Load("Craft/" + PrefixStr + SelectedID, typeof(GameObject)), hit.collider.transform.position + hit.normal, rot) as GameObject;
                        TotalVolume += instantiated.GetComponent<ID>().Volume;
                        VolumeText.text = "Total volume : " + TotalVolume;
                        TotalParts++;
                    }
                }
            }
            // paint mode
            else
            {
                MeshRenderer[] renderers = hit.collider.gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach(MeshRenderer r in renderers)
                {
                    foreach(Material m in r.materials)
                        m.color = colorPicker.col;
                }
            }
        }
    }


    private bool DFS(ID[] pieces, ID id)
    {
        if (id.Id == uint.MaxValue)
            return true;
        id.Dead = true; // use the dead variable to check if we met it already
        Collider[] col = Physics.OverlapSphere(id.transform.position, 1.45f);
        for(uint aya = 0; aya < col.Length; aya++)
        {
            ID i = col[aya].GetComponent<ID>();
            Vector3 IDpos = id.transform.position; // function ID
            Vector3 Ipos = i.transform.position;   // one of IDs around

            // if in diagonal
            if (IDpos.x == Ipos.x && IDpos.y != Ipos.y && IDpos.z != Ipos.z ||
                IDpos.y == Ipos.y && IDpos.x != Ipos.x && IDpos.z != Ipos.z ||
                IDpos.z == Ipos.z && IDpos.x != Ipos.x && IDpos.y != Ipos.y)
                continue;

            // finding closest transforms of those IDs
            float miniDist = 999;
            int[] posXY = { 0, 0 };
            for(int index1 = 0; index1 < id.CanBuild.Length; index1++)
            {
                for (int index2 = 0; index2 < i.CanBuild.Length; index2++)
                {
                    float dist = Vector3.Distance(id.CanBuild[index1].position, i.CanBuild[index2].position);
                    if (dist < miniDist)
                    {
                        miniDist = dist;
                        posXY[0] = index1;
                        posXY[1] = index2;
                    }
                }
            }

            // checking if it's ourself or if we can't reach it
            if (i == id || id.CanBuild[posXY[0]].name == "no" || i.CanBuild[posXY[1]].name == "no")
                continue;

            if (!i.Dead)
            {
                if (DFS(pieces, i))
                    return true;
                else
                    continue;
            }
        }
        return false;
    }


    public bool SaveAs()
    {
        if (ShipName.text == "")
            return false;

        ID[] allPieces = FindObjectsOfType<ID>();

        // ---------- check that the craft is correct ----------

        uint keptValue = allPieces[0].Id; // reference to which every part should be able to connect (it does not matter)
        allPieces[0].Id = uint.MaxValue;
        for (uint i = 1; i < allPieces.Length; i++)
        {
            foreach (ID ahi in allPieces)
                ahi.Dead = false;
            if (!DFS(allPieces, allPieces[i]))
            {
                Debug.LogError("index " + i + " gives an issue. LocalPosition is " + allPieces[i].transform.localPosition);
                allPieces[0].Id = keptValue;
                return false;
            }
        }
        allPieces[0].Id = keptValue;

        // actually save
        string strs = ShipName.text + '\n';
        foreach (ID piece in allPieces)
        {
            Color oof = piece.gameObject.GetComponent<MeshRenderer>().material.color;

            strs +=
                piece.Id + "_" +
                piece.transform.position.x + ":" +
                piece.transform.position.y + ":" +
                piece.transform.position.z + "_" +
                piece.transform.eulerAngles.x + ":" +
                piece.transform.eulerAngles.y + ":" +
                piece.transform.eulerAngles.z + "_" +
                oof.r + ":" +
                oof.g + ":" +
                oof.b + "\n";
        }
        saveNload.SaveAs(ShipName.text, strs);
        return true;
    }


    public void SaveNquit()
    {
        if (SaveAs())
            ExitToMenu();
    }


    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menus");
    }


    public void Test()
    {
        if (ShipName.text == "" || !SaveAs())
            return;
        GameObject value = Instantiate(DontDestroyToTest);
        value.GetComponent<DontDestroyLoad>().fileValue = saveNload.LoadAs(ShipName.text);
        SceneManager.LoadScene("TestScene");
    }


    public void DeleteThis()
    {
        string s = Application.persistentDataPath + "\\" + ShipName.text + ".chancla";
        File.Delete(s);
        ExitToMenu();
    }

    #endregion


    void Awake()
    {
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
                string r = "";
                string g = "";
                string b = "";

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

                    // get color
                    if (nb1 == 3 && nb2 == 0)
                        r += line[j];
                    if (nb1 == 3 && nb2 == 1)
                        g += line[j];
                    if (nb1 == 3 && nb2 == 2)
                        b += line[j];
                }
                SelectedID = id;
                UpdatePrefix();
                GameObject instantiated = Instantiate(
                    Resources.Load("Craft/" + PrefixStr + id, typeof(GameObject)),
                    new Vector3(float.Parse(posx), float.Parse(posy), float.Parse(posz)),
                    Quaternion.Euler(float.Parse(rotx), float.Parse(roty), float.Parse(rotz))) as GameObject;
                TotalVolume += instantiated.GetComponent<ID>().Volume;
                TotalParts++;
                MeshRenderer[] renderers = instantiated.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer re in renderers)
                {
                    foreach (Material m in re.materials)
                        m.color = new Color(float.Parse(r), float.Parse(g), float.Parse(b));
                }
            }
        }
        else
        {
            DontDestroyLoadName newship = FindObjectOfType<DontDestroyLoadName>();
            if (newship)
            {
                ShipName.text = newship.Name;
                Destroy(newship.gameObject);
            }
            Instantiate(Resources.Load("Craft/Cubes/0", typeof(GameObject)), Vector3.zero, Quaternion.identity);
            TotalParts = 1;
        }

        VolumeText.text = "Total volume : " + TotalVolume;
        SelectedID = "0";
        PrefixStr = "Cubes/";
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

        // display collider to user
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit ColHit, 30, layermask) && !ColHit.collider.transform.Find(ColliderDisplay.name))
        {
            if (currentCol)
                Destroy(currentCol.gameObject);
            currentCol = Instantiate(ColliderDisplay, ColHit.collider.transform.position + ColHit.collider.gameObject.GetComponent<BoxCollider>().center, ColHit.collider.transform.rotation, ColHit.collider.transform);
            currentCol.transform.localScale = ColHit.collider.gameObject.GetComponent<BoxCollider>().size * 1.0001f;
        }
        else if(currentCol)
            Destroy(currentCol);

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