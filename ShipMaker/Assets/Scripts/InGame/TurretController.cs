using UnityEngine;


public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 20;
    public float elevationSpeed = 20;
    public float MaxDepression = -5;
    public float MaxElevation = 70;
    public float reloadingTime = 1;
    public float BulletDiameter = 0.2f;
    public float Damage = 1;
    public Transform target;
    public Transform turret;
    public Transform gun;
    public GameObject Bullet;
    public Transform[] BulletSpawns;
    public bool Activated = true;

    private float gunRotVal;
    private float currentTime = 0;
    private bool NotPlayerControlled = false;


    private void Awake()
    {
        AiController AI = transform.parent.GetComponent<AiController>();
        if(AI)
        {
            target = AI.target;
            NotPlayerControlled = true;
            return;
        }

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

        /*
        foreach (Transform BulletSpawn in BulletSpawns)
        {
            Debug.DrawRay(BulletSpawn.position - BulletSpawn.right * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, Color.red, 1);
            Debug.DrawRay(BulletSpawn.position + BulletSpawn.right * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, Color.red, 1);
            Debug.DrawRay(BulletSpawn.position - BulletSpawn.up * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, Color.red, 1);
            Debug.DrawRay(BulletSpawn.position + BulletSpawn.up * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, Color.red, 1);
        }
        */

        // - BulletSpawn.forward * (0.5f + BulletDiameter) offesets backwards to start the ray into the turret collider
        foreach (Transform BulletSpawn in BulletSpawns)
        {
            if (NotPlayerControlled)
            {
                if ((Physics.Raycast(BulletSpawn.position - BulletSpawn.right * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, out RaycastHit hitL, 1000) &&
                    hitL.collider.CompareTag("Enemy")) ||
                    (Physics.Raycast(BulletSpawn.position + BulletSpawn.right * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, out RaycastHit hitR, 1000) &&
                    hitR.collider.CompareTag("Enemy")) ||
                    (Physics.Raycast(BulletSpawn.position - BulletSpawn.up * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, out RaycastHit hitU, 1000) &&
                    hitU.collider.CompareTag("Enemy")) ||
                    (Physics.Raycast(BulletSpawn.position + BulletSpawn.up * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, out RaycastHit hitD, 1000) &&
                    hitD.collider.CompareTag("Enemy")))
                    return;
            }
            else
            {
                if ((Physics.Raycast(BulletSpawn.position - BulletSpawn.right * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, out RaycastHit hitL, 1000) &&
                    hitL.collider.CompareTag("Untagged")) ||
                    (Physics.Raycast(BulletSpawn.position + BulletSpawn.right * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, out RaycastHit hitR, 1000) &&
                    hitR.collider.CompareTag("Untagged")) ||
                    (Physics.Raycast(BulletSpawn.position - BulletSpawn.up * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, out RaycastHit hitU, 1000) &&
                    hitU.collider.CompareTag("Untagged")) ||
                    (Physics.Raycast(BulletSpawn.position + BulletSpawn.up * BulletDiameter - BulletSpawn.forward * (0.5f + BulletDiameter), BulletSpawn.forward, out RaycastHit hitD, 1000) &&
                    hitD.collider.CompareTag("Untagged")))
                    return;
            }
        }

        currentTime = reloadingTime;

        foreach (Transform BulletSpawn in BulletSpawns)
        {
            GameObject obj = Instantiate(Bullet, BulletSpawn.position, Quaternion.identity);
            obj.GetComponent<Transform>().localScale = new Vector3(BulletDiameter, BulletDiameter, BulletDiameter);
            obj.GetComponent<Bullet>().damage = Damage;
            obj.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward * 200, ForceMode.VelocityChange);
        }
    }
}