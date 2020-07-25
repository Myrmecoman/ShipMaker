using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float damage = 1;


    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.name);
        ID id = coll.GetComponent<ID>();
        if (id)
            id.Damage(damage);
        Destroy(gameObject);
    }
}