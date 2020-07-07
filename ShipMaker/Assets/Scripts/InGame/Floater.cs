using UnityEngine;


public class Floater : MonoBehaviour
{
    public Rigidbody rb;
    public float DepthBeforeSubmerged = 1;
    public float DisplacementAmount = 3;
    public float floaterCount = 1;
    public float WaterDrag = 0.99f;
    public float WaterAngularDrag = 0.5f;


    void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        if(transform.position.y < 0)
        {
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / DepthBeforeSubmerged) * DisplacementAmount;
            rb.AddForceAtPosition(new Vector3(0, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0), transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * -rb.velocity * WaterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * WaterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}