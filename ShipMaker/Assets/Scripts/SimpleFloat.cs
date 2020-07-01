using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class SimpleFloat : MonoBehaviour
{
    private Rigidbody rb;
    private Transform[] blocsTrans;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        blocsTrans = transform.GetComponentsInChildren<Transform>();
    }


    void FixedUpdate()
    {
        foreach(Transform trans in blocsTrans)
        {
            if (trans.position.y < 0.5f)
                rb.AddForceAtPosition(new Vector3(0, 0.4f, 0), trans.position, ForceMode.Force);
        }
    }
}