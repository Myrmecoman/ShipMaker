﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ShipController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform camPosUpdate;
    public Transform camRot;
    public Transform cam;
    public float Sensivity = 0.2f;

    private bool LockMove = false;
    private string PrefixStr;
    private float maxiX = 0;
    private float miniX = 0;
    private float maxiY = 0;
    private float miniY = 0;
    private float maxiZ = 0;
    private float miniZ = 0;
    private uint Power = 0;
    private List<ChimneyStat> chimneys = new List<ChimneyStat>();
    private List<Propeller> propellers = new List<Propeller>();
    private List<Rudder> rudders = new List<Rudder>();

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

    #endregion


    void Awake()
    {
        DontDestroyLoad obj = FindObjectOfType<DontDestroyLoad>();
        if (obj)
        {
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

                UpdatePrefix(id);
                Instantiate(
                    Resources.Load("Craft/" + PrefixStr + id, typeof(GameObject)),
                    new Vector3(float.Parse(posx), float.Parse(posy), float.Parse(posz)),
                    Quaternion.Euler(float.Parse(rotx), float.Parse(roty), float.Parse(rotz)),
                    transform);
            }
            transform.position = new Vector3(0, -miniY, 0);
        }
        else
            Instantiate(Resources.Load("Craft/Cubes/0", typeof(GameObject)), Vector3.zero, Quaternion.identity, transform);

        // manage children
        Transform[] children = GetComponentsInChildren<Transform>();
        for (uint i = 1; i < children.Length; i++)
        {
            if (children[i].GetComponent<Floater>())
            {
                children[i].GetComponent<Floater>().enabled = true;
                children[i].GetComponentInChildren<Floater>().rb = rb;
            }

            if (children[i].GetComponent<ChimneyStat>())
                chimneys.Add(children[i].GetComponentInChildren<ChimneyStat>());

            if (children[i].GetComponent<ID>())
                rb.mass += children[i].GetComponent<ID>().Weight;
            
            if (children[i].GetComponent<Propeller>())
                propellers.Add(children[i].GetComponentInChildren<Propeller>());

            if (children[i].GetComponent<Rudder>())
                rudders.Add(children[i].GetComponentInChildren<Rudder>());
        }
        UpdateChimneys();

        // set camera center
        camPosUpdate.localPosition = new Vector3((miniX + maxiX) / 2, (miniY + maxiY) / 2, (miniZ + maxiZ) / 2);
        camRot.localPosition = camPosUpdate.localPosition;
        camRot.localEulerAngles = new Vector3(20, 0, 0);
        cam.localPosition = new Vector3(0, 0, -(miniZ + maxiZ) / 2 - 20);
    }


    private void UpdatePrefix(string id)
    {
        if (int.Parse(id) < 200)
        {
            PrefixStr = "Cubes/";
            return;
        }
        if (int.Parse(id) < 400)
        {
            PrefixStr = "Weapons/";
            return;
        }
        if (int.Parse(id) < 600)
        {
            PrefixStr = "Engines/";
            return;
        }
        if (int.Parse(id) < 800)
        {
            PrefixStr = "Cosmetics/";
            return;
        }
    }


    public void UpdateChimneys()
    {
        Power = 0;
        foreach(ChimneyStat ch in chimneys)
            Power += ch.Power;
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        camRot.position = camPosUpdate.position;
        if (!LockMove)
        {
            camRot.eulerAngles = new Vector3(camRot.eulerAngles.x - WantMouse.y * Sensivity, camRot.eulerAngles.y + WantMouse.x * Sensivity, 0);
        }
    }


    void FixedUpdate()
    {
        if (!LockMove)
        {
            foreach (Propeller prop in propellers)
            {
                if (prop.transform.position.y <= 0 && prop.Activated)
                    rb.AddForceAtPosition(prop.transform.forward * prop.PowerMultiplier * (Power / propellers.Count) * WantMove.y, prop.transform.position, ForceMode.Force);
            }
            foreach (Rudder rud in rudders)
            {
                if (rud.transform.position.y <= 0 && rud.Activated)
                    rb.AddForceAtPosition(rud.transform.right * 200 * rb.velocity.magnitude * -WantMove.x, rud.transform.position, ForceMode.Force);
            }
        }
    }
}