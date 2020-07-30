using UnityEngine;


public class GizmoAngle : MonoBehaviour
{
    public Transform copyRot;
    public Transform Gizmo;
    public Transform ObjPos;

    private GameObject PreviewObj;


    private void Awake()
    {
        PreviewObj = Instantiate(Resources.Load("Craft/Cubes/0", typeof(GameObject)), ObjPos.position, Quaternion.identity) as GameObject;
        Destroy(PreviewObj.GetComponent<ID>());
        PreviewObj.layer = 10;
        PreviewObj.transform.localScale *= 12; // Clamp the size of each object to 12
    }


    void Update()
    {
        Gizmo.eulerAngles = new Vector3(0, -copyRot.eulerAngles.y, 0);
        PreviewObj.transform.eulerAngles = new Vector3(0, -copyRot.eulerAngles.y, 0);
    }


    public void ChangeObj(string id)
    {
        Destroy(PreviewObj.gameObject);
        PreviewObj = Instantiate(Resources.Load("Craft/" + UpdatePrefix(id) + id, typeof(GameObject)), ObjPos.position, Quaternion.identity) as GameObject;
        Destroy(PreviewObj.GetComponent<ID>());
        float max = Mathf.Max(
            PreviewObj.GetComponent<BoxCollider>().size.x,
            PreviewObj.GetComponent<BoxCollider>().size.y,
            PreviewObj.GetComponent<BoxCollider>().size.z);
        PreviewObj.transform.localScale = new Vector3(
            PreviewObj.transform.localScale.x * (12 / max),
            PreviewObj.transform.localScale.y * (12 / max),
            PreviewObj.transform.localScale.z * (12 / max));
        foreach (Transform t in PreviewObj.transform.GetComponentsInChildren<Transform>())
            t.gameObject.layer = 10;
    }


    private string UpdatePrefix(string id)
    {
        if (int.Parse(id) < 200)
            return "Cubes/";
        if (int.Parse(id) < 400)
            return "Weapons/";
        if (int.Parse(id) < 600)
            return "Engines/";
        if (int.Parse(id) < 800)
            return "Cosmetics/";
        return "ERROR : gizmoScript, invalid ID";
    }
}