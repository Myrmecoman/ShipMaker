using UnityEngine;


public class GizmoAngle : MonoBehaviour
{
    public Transform copyRot;


    void Update()
    {
        transform.eulerAngles = new Vector3(0, -copyRot.eulerAngles.y, 0);
    }
}