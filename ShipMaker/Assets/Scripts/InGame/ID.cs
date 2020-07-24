using UnityEngine;


public class ID : MonoBehaviour
{
    public uint Id = 0;
    public float hp = 10;
    public uint Weight = 1;
    public uint Volume = 1;
    [Header("order : front, back, up, down, right, left")]
    public bool[] CanBuild = {true, true, true, true, true, true};

    private bool Dead = false;


    public void Damage(float dmg)
    {
        hp -= dmg;
        if(hp <= 0 && !Dead)
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