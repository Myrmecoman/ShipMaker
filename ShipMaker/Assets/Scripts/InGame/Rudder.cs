using UnityEngine;


public class Rudder : MonoBehaviour
{
    public float Strength = 1;
    public Transform RudderAxis;
    public bool Activated = true;
    [HideInInspector] public float dir;

    private float angle;


    private void Update()
    {
        angle = RudderAxis.localEulerAngles.y - dir * Time.deltaTime * 50;
        if (angle > 180f)
            angle -= 360;
        angle = Mathf.Clamp(angle, -45, 45);
        RudderAxis.localEulerAngles = new Vector3(0, angle, 0);
    }
}