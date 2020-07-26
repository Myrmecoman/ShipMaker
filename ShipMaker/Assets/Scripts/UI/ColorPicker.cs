using UnityEngine;
using UnityEngine.UI;


public class ColorPicker : MonoBehaviour
{
    public Texture2D colorPicker;
    public Rect colorPanelRect;
    public Color col;
    public GameObject helpScreen;
    public Text BuildMode;


    private void Awake()
    {
        colorPanelRect.x = Screen.width - colorPanelRect.width;
        colorPanelRect.y = colorPanelRect.height - 120;
    }

    void OnGUI()
    {
        if (helpScreen.activeSelf)
            return;

        GUI.DrawTexture(colorPanelRect, colorPicker);
        if (GUI.RepeatButton(colorPanelRect, ""))
        {
            Vector2 pickpos = Event.current.mousePosition;
            float aaa = pickpos.x - colorPanelRect.x;
            float bbb = pickpos.y - colorPanelRect.y;
            int aaa2 = (int)(aaa * (colorPicker.width / (colorPanelRect.width + 0.0f)));
            int bbb2 = (int)((colorPanelRect.height - bbb) * (colorPicker.height / (colorPanelRect.height + 0.0f)));
            col = colorPicker.GetPixel(aaa2, bbb2);
            if(BuildMode.text != "Paint mode")
                BuildMode.text = "Paint mode";
        }
    }
}