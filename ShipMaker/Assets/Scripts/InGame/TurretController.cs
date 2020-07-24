using UnityEngine;


public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 20;
    public float elevationSpeed = 20;
    public float MaxDepression = -5;
    public float MaxElevation = 70;
    public float reloadingTime = 1;
    public Transform target;
    public Transform turret;
    public Transform gun;
    public GameObject Bullet;
    public Transform BulletSpawn;
    public bool Activated = true;

    private float gunRotVal;
    private float currentTime = 0;


    private void Awake()
    {
        ShipController ship = FindObjectOfType<ShipController>();
        if(ship)
            target = ship.target;
    }


    void Update()
    {
        if(!Activated)
            return;

        if (currentTime > 0)
            currentTime -= Time.deltaTime;

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
        gunRotVal = rot.eulerAngles.x;
        if (gunRotVal > 180f)
            gunRotVal -= 360;
        gunRotVal = Mathf.Clamp(gunRotVal, -MaxElevation, -MaxDepression);
        rot.eulerAngles = new Vector3(gunRotVal, 0, 0);
        gun.localRotation = rot;
    }


    public void Shoot()
    {
        if (currentTime > 0 || !Activated)
            return;

        currentTime = reloadingTime;
        GameObject obj = Instantiate(Bullet, BulletSpawn.position, Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward * 200, ForceMode.VelocityChange);
    }
}