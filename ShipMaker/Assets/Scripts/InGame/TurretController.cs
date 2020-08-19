using UnityEngine;
using UnityEngine.SceneManagement;


public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 20;
    public float elevationSpeed = 20;
    public float MaxDepression = -5;
    public float MaxElevation = 70;
    public float reloadingTime = 1;
    public float BulletDiameter = 0.2f;
    public float BulletForceMultiplier = 1;
    public float Damage = 1;
    public Transform target;
    public Transform turret;
    public Transform gun;
    public GameObject Bullet;
    public Transform[] BulletSpawns;
    public bool Activated = true;

    private float gunRotVal;
    private float currentTime = 0;
    private float xzSizeby2 = 0.5f;
    private bool NotPlayerControlled = false;


    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Craft")
        {
            Activated = false;
            return;
        }

        AiController AI = transform.parent.GetComponent<AiController>();
        if (AI)
        {
            target = AI.target;
            NotPlayerControlled = true;
        }
        else
        {
            ShipController ship = FindObjectOfType<ShipController>();
            if (ship)
                target = ship.target;
        }
        xzSizeby2 = (GetComponent<BoxCollider>().size.x + GetComponent<BoxCollider>().size.z) / 4;
    }


    void Update()
    {
        if(!Activated || BulletSpawns[0].position.y < 0)
            return;

        if (currentTime > 0)
            currentTime -= Time.deltaTime;

        foreach (Transform BulletSpawn in BulletSpawns)
        {
            Debug.DrawRay(BulletSpawn.position - BulletSpawn.right * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, Color.red, 0.1f);
            Debug.DrawRay(BulletSpawn.position + BulletSpawn.right * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, Color.red, 0.1f);
            Debug.DrawRay(BulletSpawn.position - BulletSpawn.up * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, Color.red, 0.1f);
            Debug.DrawRay(BulletSpawn.position + BulletSpawn.up * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, Color.red, 0.1f);
        }

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

        // - BulletSpawn.forward * (0.5f + BulletDiameter) offesets backwards to start the ray into the turret collider
        foreach (Transform BulletSpawn in BulletSpawns)
        {
            if (NotPlayerControlled)
            {
                if ((Physics.Raycast(BulletSpawn.position - BulletSpawn.right * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, out RaycastHit hitL, 1000) &&
                    hitL.collider.CompareTag("Enemy")) ||
                    (Physics.Raycast(BulletSpawn.position + BulletSpawn.right * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, out RaycastHit hitR, 1000) &&
                    hitR.collider.CompareTag("Enemy")) ||
                    (Physics.Raycast(BulletSpawn.position - BulletSpawn.up * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, out RaycastHit hitU, 1000) &&
                    hitU.collider.CompareTag("Enemy")) ||
                    (Physics.Raycast(BulletSpawn.position + BulletSpawn.up * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, out RaycastHit hitD, 1000) &&
                    hitD.collider.CompareTag("Enemy")))
                    return;
            }
            else
            {
                if ((Physics.Raycast(BulletSpawn.position - BulletSpawn.right * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, out RaycastHit hitL, 1000) &&
                    hitL.collider.CompareTag("Untagged")) ||
                    (Physics.Raycast(BulletSpawn.position + BulletSpawn.right * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, out RaycastHit hitR, 1000) &&
                    hitR.collider.CompareTag("Untagged")) ||
                    (Physics.Raycast(BulletSpawn.position - BulletSpawn.up * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, out RaycastHit hitU, 1000) &&
                    hitU.collider.CompareTag("Untagged")) ||
                    (Physics.Raycast(BulletSpawn.position + BulletSpawn.up * BulletDiameter/2 - BulletSpawn.forward * xzSizeby2, BulletSpawn.forward, out RaycastHit hitD, 1000) &&
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
            obj.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward * 200 * BulletForceMultiplier, ForceMode.VelocityChange);
        }
    }
}