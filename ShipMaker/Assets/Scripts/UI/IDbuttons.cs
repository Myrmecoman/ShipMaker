using UnityEngine;
using UnityEngine.UI;


public class IDbuttons : MonoBehaviour
{
    public uint Id = 0;


    public void CraftCallID()
    {
        CraftCam cam = FindObjectOfType<CraftCam>();
        cam.SelectedID = Id.ToString();
        cam.SelectedElement.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
    }
}