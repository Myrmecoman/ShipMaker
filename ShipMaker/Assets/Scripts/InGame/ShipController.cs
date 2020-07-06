using System.IO;
using UnityEngine;


public class ShipController : MonoBehaviour
{
    public Rigidbody rb;

    private float miniY = 0;
    private float dragToApply = 0; 

    // Inputs
    [HideInInspector] public Vector2 WantMove = Vector2.zero;
    [HideInInspector] public Vector2 WantMouse = Vector2.zero;
    [HideInInspector] public Vector2 WantScroll = Vector2.zero;
    [HideInInspector] public bool RightClickHold;
    [HideInInspector] public bool LeftClickHold;


    #region Input Functions

    public void PressEscape()
    {

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
                // modify Y instanciation
                if (float.Parse(posy) < miniY)
                    miniY = float.Parse(posy);

                Instantiate(
                    Resources.Load("Craft/Cubes/" + id, typeof(GameObject)),
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
                dragToApply++;
                children[i].GetComponentInChildren<Floater>().rb = rb;
            }
        }

        // tweaks to stabilise the shits
        rb.drag = dragToApply * 0.15f;
        rb.angularDrag = dragToApply * 0.1f;
    }


    void Update()
    {
        
    }


    void FixedUpdate()
    {
        
    }
}