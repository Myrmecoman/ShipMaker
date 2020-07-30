using UnityEngine;


public class IDbuttons : MonoBehaviour
{
    public uint Id = 0;

    private CraftCam craftCam;
    private GizmoAngle gizmoScript;


    private void Awake()
    {
        craftCam = FindObjectOfType<CraftCam>();
        gizmoScript = FindObjectOfType<GizmoAngle>();
    }


    public void CraftCallID()
    {
        craftCam.SelectedID = Id.ToString();
        gizmoScript.ChangeObj(Id.ToString());
    }
}