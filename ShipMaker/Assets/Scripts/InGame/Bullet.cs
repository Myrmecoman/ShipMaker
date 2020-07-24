using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float damage = 1;


    private void OnCollisionEnter(Collision coll)
    {
        Debug.Log(coll.gameObject.name);
        ID id = coll.gameObject.GetComponent<ID>();
        if(id)
            id.Damage(damage);
        Destroy(gameObject);
    }
}