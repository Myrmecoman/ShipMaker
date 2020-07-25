using UnityEngine;


public class AnchorPointsUI : MonoBehaviour
{
    public RectTransform parent;
    public float distance = 0;
    public bool ShiftX = false;
    public bool ShiftY = false;


    void Awake()
    {
        Vector3 rect = GetComponent<RectTransform>().localPosition;
        if (ShiftX)
            GetComponent<RectTransform>().localPosition = new Vector3(distance * parent.rect.width, rect.y, rect.z);
        if (ShiftY)
            GetComponent<RectTransform>().localPosition = new Vector3(rect.x, distance * parent.rect.height - 93, rect.z);
    }
}