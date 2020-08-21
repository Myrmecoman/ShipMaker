using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ShipController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform camPosUpdate;
    public Transform camRot;
    public Transform cam;
    public Transform target;
    public float Sensivity = 0.2f;
    public SteerLerp steerUI;
    public ThrottleLerp throttleUI;
    public Text SpeedTxt;
    public LayerMask layermask;

    private bool LockMove = false;
    private float ChangedSensivity;
    private string PrefixStr;
    private float maxiX = 0;
    private float miniX = 0;
    private float maxiY = 0;
    private float miniY = 0;
    private float maxiZ = 0;
    private float miniZ = 0;
    private uint TotalPrice = 0;
    private float CamRotVal;
    private float Speed;
    private uint Power = 0;
    private PlayerInfos infos;
    private List<ChimneyStat> chimneys = new List<ChimneyStat>();
    private List<Propeller> propellers = new List<Propeller>();
    private List<Rudder> rudders = new List<Rudder>();
    private List<TurretController> turrets = new List<TurretController>();

    // Inputs
    [HideInInspector] public Vector2 WantMove = Vector2.zero;
    [HideInInspector] public Vector2 WantMouse = Vector2.zero;
    [HideInInspector] public Vector2 WantScroll = Vector2.zero;
    [HideInInspector] public bool RightClickHold;
    [HideInInspector] public bool LeftClickHold;


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


    public void MoveInput(Vector2 move)
    {
        throttleUI.input = move.y;
    }

    #endregion


    #region Initialization

    void Awake()
    {
        ChangedSensivity = Sensivity;
        DontDestroyLoad obj = FindObjectOfType<DontDestroyLoad>();
        if (obj)
        {
            float NbElements = 0;
            string str = obj.fileValue;
            Destroy(obj.gameObject);
            StringReader reader = new StringReader(str);
            FindObjectOfType<TestUI>().load = reader.ReadLine();
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
                // get max and mini values
                if (float.Parse(posx) < miniX)
                    miniX = float.Parse(posx);
                if (float.Parse(posx) > maxiX)
                    maxiX = float.Parse(posx);
                if (float.Parse(posy) < miniY)
                    miniY = float.Parse(posy);
                if (float.Parse(posy) > maxiY)
                    maxiY = float.Parse(posy);
                if (float.Parse(posz) < miniZ)
                    miniZ = float.Parse(posz);
                if (float.Parse(posz) > maxiZ)
                    maxiZ = float.Parse(posz);

                NbElements++;
                UpdatePrefix(id);
                GameObject instantiated = Instantiate(
                    Resources.Load("Craft/" + PrefixStr + id, typeof(GameObject)),
                    new Vector3(float.Parse(posx), float.Parse(posy), float.Parse(posz)),
                    Quaternion.Euler(float.Parse(rotx), float.Parse(roty), float.Parse(rotz)),
                    transform) as GameObject;
                MeshRenderer[] renderers = instantiated.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer re in renderers)
                {
                    foreach (Material m in re.materials)
                        m.color = new Color(float.Parse(r), float.Parse(g), float.Parse(b));
                }
            }
            transform.position = new Vector3(0, -miniY, 0);
        }
        else
        {
            Instantiate(Resources.Load("Craft/Cubes/0", typeof(GameObject)), Vector3.zero, Quaternion.identity, transform);
        }

        // manage children
        Transform[] children = GetComponentsInChildren<Transform>();
        for (uint i = 1; i < children.Length; i++)
        {
            if (children[i].GetComponent<Floater>())
            {
                children[i].GetComponent<Floater>().enabled = true;
                children[i].GetComponent<Floater>().rb = rb;
            }

            if (children[i].GetComponent<TurretController>())
            {
                children[i].GetComponent<TurretController>().enabled = true;
                turrets.Add(children[i].GetComponent<TurretController>());
            }

            if (children[i].GetComponent<Propeller>())
            {
                children[i].GetComponent<Propeller>().Boat = rb;
                propellers.Add(children[i].GetComponent<Propeller>());
            }

            if (children[i].GetComponent<ID>())
            {
                rb.mass += children[i].GetComponent<ID>().Weight;
                TotalPrice += children[i].GetComponent<ID>().Price;
                foreach (Transform t in children[i].GetComponent<ID>().CanBuild)
                    Destroy(t.gameObject);
            }

            if (children[i].GetComponent<ChimneyStat>())
                chimneys.Add(children[i].GetComponent<ChimneyStat>());

            if (children[i].GetComponent<Rudder>())
                rudders.Add(children[i].GetComponent<Rudder>());
        }

        

        UpdateChimneys();

        // set camera center
        camPosUpdate.localPosition = new Vector3((miniX + maxiX) / 2, (miniY + maxiY) / 2, (miniZ + maxiZ) / 2);
        camRot.position = camPosUpdate.position;
        camRot.localEulerAngles = new Vector3(20, 0, 0);
        cam.localPosition = new Vector3(0, 5, -(maxiZ - miniZ) / 2 - 10);
    }

    #endregion


    private void UpdatePrefix(string id)
    {
        int parsed = int.Parse(id);
        if (parsed < 200)
        {
            PrefixStr = "Cubes/";
            return;
        }
        if (parsed < 400)
        {
            PrefixStr = "Weapons/";
            return;
        }
        if (parsed < 600)
        {
            PrefixStr = "Engines/";
            return;
        }
        if (parsed < 800)
        {
            PrefixStr = "Cosmetics/";
            return;
        }
    }


    public void UpdateChimneys()
    {
        Power = 0;
        foreach (ChimneyStat ch in chimneys)
        {
            if(ch.Activated)
                Power += ch.Power;
        }
    }


    void Start()
    {
        infos = FindObjectOfType<PlayerInfos>();
        if (TotalPrice > infos.maxPriceAllowed)
        {
            Debug.Log(TotalPrice);
            string s = Application.persistentDataPath + "\\" + FindObjectOfType<TestUI>().load + ".chancla";
            File.Delete(s);
            SceneManager.LoadScene("Menus");
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (transform.position.y < -30)
        {
            Debug.Log("lose");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Menus");
            return;
        }

        camRot.position = camPosUpdate.position;

        Speed = rb.velocity.magnitude * 1.944f /*means 3.6 * 0.54*/;
        SpeedTxt.text = String.Format("{0:0.0}", Speed) + " knots";

        if (LockMove)
            return;

        steerUI.input = WantMove.x;

        // adapt sensivity to fov
        ChangedSensivity = Sensivity * cam.GetComponent<Camera>().fieldOfView / 60;

        // camera movements
        CamRotVal = Mathf.Clamp(CamRotVal - WantMouse.y * ChangedSensivity, -89, 89);
        camRot.eulerAngles = new Vector3(360 + CamRotVal, camRot.eulerAngles.y + WantMouse.x * ChangedSensivity, 0);

        // change fov
        cam.GetComponent<Camera>().fieldOfView = Mathf.Clamp(cam.GetComponent<Camera>().fieldOfView - WantScroll.y * 1 / 20, 10, 90);

        // target for weaponery
        if (Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out RaycastHit hit, float.PositiveInfinity, layermask))
            target.position = hit.point;
        else
            target.position = cam.position + cam.TransformDirection(Vector3.forward).normalized * 1000;
    }


    void FixedUpdate()
    {
        UpdateChimneys();

        // propellers
        foreach (Propeller prop in propellers)
        {
            if(Speed > 50 && prop.transform.position.y <= 0 && prop.Activated)
                rb.AddForceAtPosition(prop.transform.forward * prop.PowerMultiplier * (Power * (50/Speed) / propellers.Count) * throttleUI.value, prop.transform.position, ForceMode.Force);
            else if (prop.transform.position.y <= 0 && prop.Activated)
                rb.AddForceAtPosition(prop.transform.forward * prop.PowerMultiplier * (Power / propellers.Count) * throttleUI.value, prop.transform.position, ForceMode.Force);
        }

        // rudders
        foreach (Rudder rud in rudders)
        {
            if (rud.transform.position.y <= 0 && rud.Activated)
            {
                rb.AddForceAtPosition(rud.transform.right * rud.Strength * 100 * rb.velocity.magnitude * steerUI.value, rud.transform.position, ForceMode.Force);
                rud.dir = steerUI.value;
            }
        }

        if (LockMove)
            return;

        // firing
        if (LeftClickHold)
        {
            foreach (TurretController t in turrets)
                t.Shoot();
        }
    }
}