using UnityEngine;


public class IDbuttons : MonoBehaviour
{
    public uint Id = 0;


    public void CraftCallID()
    {
        FindObjectOfType<CraftCam>().SelectedCubeID = Id.ToString();
    }
}