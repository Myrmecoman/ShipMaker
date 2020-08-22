using UnityEngine;


public class ID : MonoBehaviour
{
    public uint Id = 0;
    public float hp = 10;
    public uint Weight = 1;
    public uint Price = 1;
    [Header("order : front, back, up, down, right, left")]
    public Transform[] CanBuild;
    [HideInInspector] public bool Dead = false;

    private float InitialHp;


    void Awake()
    {
        InitialHp = hp;
    }


    public void Damage(float dmg)
    {
        if (Dead)
            return;

        hp -= dmg;

        if (hp/InitialHp <= 0)
        {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer re in renderers)
            {
                foreach (Material m in re.materials)
                    m.SetTexture("_MainTex", Resources.Load("Textures/burnt3") as Texture);
            }
            Destroy(GetComponent<BoxCollider>());
        }
        else if (hp / InitialHp <= 0.34f)
        {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer re in renderers)
            {
                foreach (Material m in re.materials)
                    m.SetTexture("_MainTex", Resources.Load("Textures/burnt2") as Texture);
            }
        }
        else if (hp / InitialHp <= 0.67f)
        {
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer re in renderers)
            {
                foreach (Material m in re.materials)
                    m.SetTexture("_MainTex", Resources.Load("Textures/burnt1") as Texture);
            }
        }

        if(hp <= 0)
        {
            Dead = true;

            if(GetComponent<TurretController>())
                GetComponent<TurretController>().Activated = false;

            if (GetComponent<Rudder>())
                GetComponent<Rudder>().Activated = false;

            if (GetComponent<ChimneyStat>())
                GetComponent<ChimneyStat>().Activated = false;

            if (GetComponent<Propeller>())
                GetComponent<Propeller>().Activated = false;

            if (GetComponentInChildren<Floater>())
                GetComponentInChildren<Floater>().floaterCount = 0.1f;
        }
    }
}