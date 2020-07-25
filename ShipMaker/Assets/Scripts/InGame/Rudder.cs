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
        RudderAxis.localEulerAngles = new Vector3(0, 45 * dir, 0);
    }
}