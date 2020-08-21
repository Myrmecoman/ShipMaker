﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AiController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform PlayerShip;
    public Transform target;
    public LayerMask layermask;

    private float timeBetweenShots = 5;
    private float timeGap = 4;
    private float currentTime = 0;
    private string PrefixStr;
    private float maxiY = 0;
    private float miniY = 0;
    private float Speed;
    private uint Power = 0;
    private PlayerInfos infos;
    private List<ChimneyStat> chimneys = new List<ChimneyStat>();
    private List<Propeller> propellers = new List<Propeller>();
    private List<Rudder> rudders = new List<Rudder>();
    private List<TurretController> turrets = new List<TurretController>();


    #region Initialization

    void Awake()
    {
        DontDestroyLoadName obj = FindObjectOfType<DontDestroyLoadName>();
        if (obj)
        {
            float NbElements = 0;
            string str = obj.NameShip;
            Destroy(obj.gameObject);
            StringReader reader = new StringReader(str);
            reader.ReadLine();
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
                if (float.Parse(posy) < miniY)
                    miniY = float.Parse(posy);
                if (float.Parse(posy) > maxiY)
                    maxiY = float.Parse(posy);

                NbElements++;
                UpdatePrefix(id);
                GameObject instantiated = Instantiate(
                    Resources.Load("Craft/" + PrefixStr + id, typeof(GameObject)),
                    new Vector3(float.Parse(posx) + transform.position.x, float.Parse(posy) + transform.position.y, float.Parse(posz) + transform.position.z),
                    Quaternion.Euler(float.Parse(rotx), float.Parse(roty), float.Parse(rotz)),
                    transform) as GameObject;
                instantiated.tag = "Enemy";
                MeshRenderer[] renderers = instantiated.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer re in renderers)
                {
                    foreach (Material m in re.materials)
                        m.color = new Color(float.Parse(r), float.Parse(g), float.Parse(b));
                }
            }
            transform.position = new Vector3(0, -miniY, -transform.position.z);
            transform.eulerAngles = new Vector3(0, 180, 0);
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
                foreach (Transform t in children[i].GetComponent<ID>().CanBuild)
                    Destroy(t.gameObject);
            }

            if (children[i].GetComponent<ChimneyStat>())
                chimneys.Add(children[i].GetComponent<ChimneyStat>());

            if (children[i].GetComponent<Rudder>())
                rudders.Add(children[i].GetComponent<Rudder>());
        }

        UpdateChimneys();
    }

    #endregion


    void Start()
    {
        infos = FindObjectOfType<PlayerInfos>();
    }


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
            if (ch.Activated)
                Power += ch.Power;
        }
    }


    void Update()
    {
        if (transform.position.y < -30)
        {
            Debug.Log("won");
            infos.ChangeAIdefeated();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Menus");
            return;
        }

        Speed = rb.velocity.magnitude * 1.944f /*means 3.6 * 0.54 (knots)*/;

        // manage target position
        target.position = PlayerShip.position;

        // firing
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = timeBetweenShots + Random.Range(-timeGap / 2, timeGap / 2);
            foreach (TurretController t in turrets)
                t.Shoot();
        }
    }


    void FixedUpdate()
    {
        UpdateChimneys();

        // propellers
        if (Vector3.Distance(transform.position, PlayerShip.position) > 100)
        {
            foreach (Propeller prop in propellers)
            {
                if (Speed > 50 && prop.transform.position.y <= 0 && prop.Activated)
                    rb.AddForceAtPosition(prop.transform.forward * prop.PowerMultiplier * (Power * (50 / Speed) / propellers.Count) * 1 /* change this */, prop.transform.position, ForceMode.Force);
                else if (prop.transform.position.y <= 0 && prop.Activated)
                    rb.AddForceAtPosition(prop.transform.forward * prop.PowerMultiplier * (Power / propellers.Count) * 1 /* change this */, prop.transform.position, ForceMode.Force);
            }
        }

        float angle = Vector3.SignedAngle(PlayerShip.position - transform.position, transform.forward, Vector3.up);
        float RudDir;
        if (angle > 0)
            RudDir = 1;
        else
            RudDir = -1;

        // rudders
        foreach (Rudder rud in rudders)
        {
            if (rud.transform.position.y <= 0 && rud.Activated)
            {
                rb.AddForceAtPosition(rud.transform.right * rud.Strength * 100 * rb.velocity.magnitude * RudDir, rud.transform.position, ForceMode.Force);
                rud.dir = RudDir;
            }
        }
    }
}