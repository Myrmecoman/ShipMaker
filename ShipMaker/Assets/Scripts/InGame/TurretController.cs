using UnityEngine;


public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 20;
    public float elevationSpeed = 20;
    public Transform target;
    public Transform turret;
    public Transform gun;


    private void Awake()
    {
        ShipController ship = FindObjectOfType<ShipController>();
        if(ship)
            target = ship.target;
    }


    void Update()
    {
        RotateTurret(rotationSpeed);
        RotateGun(elevationSpeed);
    }


    void RotateTurret(float rotationSpeed)
    {
        Vector3 D = turret.parent.InverseTransformDirection(target.position - turret.position);
        Quaternion rot = Quaternion.RotateTowards(turret.localRotation, Quaternion.LookRotation(D), rotationSpeed * Time.deltaTime);
        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
        turret.localRotation = rot;
    }


    void RotateGun(float traverseSpeed)
    {
        Vector3 D = gun.parent.InverseTransformDirection(target.position - gun.position);
        Quaternion rot = Quaternion.RotateTowards(gun.localRotation, Quaternion.LookRotation(D), traverseSpeed * Time.deltaTime);
        rot.eulerAngles = new Vector3(rot.eulerAngles.x, 0, 0);
        gun.localRotation = rot;
    }
}