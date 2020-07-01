using UnityEngine;


public class ShipController : MonoBehaviour
{
    void Start()
    {
        // attach all children
        Rigidbody[] children = GetComponentsInChildren<Rigidbody>(true);
        for (uint i = 0; i < children.Length; i++)
        {
            if (i != 0)
            {
                FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = children[i];
                joint.breakForce = 10000;
            }
        }
    }


    void FixedUpdate()
    {
        
    }
}