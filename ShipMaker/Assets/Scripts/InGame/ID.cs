using UnityEngine;


public class ID : MonoBehaviour
{
    public uint Id = 0;
    public uint Weight = 1;
    public uint Volume = 1;
    [Header("order : front, back, up, down, right, left")]
    public bool[] CanBuild = {true, true, true, true, true, true};
}