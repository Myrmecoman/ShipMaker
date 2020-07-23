using UnityEngine;


public class DepthFollower : MonoBehaviour
{
    public Transform followed;


    void Update()
    {
        transform.position = new Vector3(followed.position.x, 0, followed.position.z);
    }
}